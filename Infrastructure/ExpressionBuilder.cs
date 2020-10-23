using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ExpressionBuilderNetCore.Common;
using ExpressionBuilderNetCore.Conditions;
using ExpressionBuilderNetCore.Generics;
using ExpressionBuilderNetCore.Interfaces;
using mambuquery.api.Core.Features.Student.Queries;

namespace mambuquery.api.Infrastructure
{
    public class ExpressionBuilder<T> where T : class
    {
        public static Expression<Func<T, bool>> Build(List<ListFilter> filters)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression expression = Expression.Default(typeof(T));
            if(filters.Count > 0)
            {
                expression = GetExpression(param, filters[0]);

                foreach(ListFilter filter in filters.GetRange(1, filters.Count() -1))
                {
                    expression = Expression.AndAlso(expression, GetExpression(param, filter));
                }

                return Expression.Lambda<Func<T, bool>>(expression, param);
            }
            return null;

        }

        private static Expression GetExpression(ParameterExpression param, ListFilter filter)
        {
            var member = Expression.Property(param, filter.Selection);

            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);
            if (!converter.CanConvertFrom(typeof(string)))
                throw new NotSupportedException();
            //will give the integer value if the string is integer
            var propertyValue1 = converter.ConvertFromInvariantString(filter.Value);
            var propertyValue2 = string.IsNullOrEmpty(filter.Value2) ? null : converter.ConvertFromInvariantString(filter.Value2);


            var constant = Expression.Constant(propertyValue1);
            var constant2 = Expression.Constant(propertyValue2);


            var emptyConstant = Expression.Constant(null);

            var arrayContainsMethod = typeof(List<object>).GetMethod("Contains", new Type[] { typeof(List<object>) });




            Expression ex = Expression.Empty();

            switch(filter.Element)
            {
                case "EQUALS":
                    ex =  EqualsTo.Build(member, constant);
                    break;
                case "STARTS_WITH":
                    ex = StartsWith.Build(member, constant);
                    break;
                case "ENDS_WITH":
                    ex = EndsWith.Build(member, constant);
                    break;
                case "CONTAINS":
                    ex = Contains.Build(member, constant);
                    break;
                case "EMPTY":
                    ex = Empty.Build(member, emptyConstant);
                    break;
                case "NOT_EMPTY":
                    ex = NotEmpty.Build(member, emptyConstant);
                    break;
                case "GREATER_THAN":
                    ex = Expression.GreaterThan(member, Expression.Convert(constant, member.Type));
                    break;
                case "LESS_THAN":
                    ex = Expression.LessThan(member, Expression.Convert(constant, member.Type));
                    break;
                case "BETWEEN":
                    ex = Between.Build(member, constant, constant2);
                    break;
                case "IN":
                    ex = Expression.Call(member, arrayContainsMethod, constant);
                    break;
            }

            return ex;

        }


        public static (object, object) GetConvertedValue(string propertyName, string value1, string value2)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);
            if (!converter.CanConvertFrom(typeof(string)))
                throw new NotSupportedException();
            //will give the integer value if the string is integer
            var propertyValue1 = converter.ConvertFromInvariantString(value1);
            var propertyValue2 = value2 == null ? null : converter.ConvertFromInvariantString(value2);

            return (propertyValue1, propertyValue2);
        }

        public static Filter<T> BuildQueryFilter(List<ListFilter> filters)
        {
            var operationMap = new Dictionary<string, ICondition>
             {
                 {"EQUALS", Condition.EqualTo},
                 {"STARTS_WITH", Condition.StartsWith},
                 {"ENDS_WITH", Condition.EndsWith},
                 {"CONTAINS", Condition.Contains},
                 {"EMPTY", Condition.IsEmpty},
                 {"NOT_EMPTY", Condition.IsNotEmpty},
                 {"GREATER_THAN", Condition.GreaterThan},
                 {"LESS_THAN", Condition.LessThan}
             };


            var filter = new Filter<T>();
            foreach (var clientFilter in filters)
            {
                var operation = operationMap[clientFilter.Element];
                var values = GetConvertedValue(clientFilter.Selection, clientFilter.Value, clientFilter.Value2);
                if (clientFilter.Value2 != null)
                {
                    filter.By(clientFilter.Selection, operation, values.Item1, values.Item2, Connector.And);
                }
                filter.By(clientFilter.Selection, operation, 3, Connector.And);
            }

            return filter;
        }


    }
}