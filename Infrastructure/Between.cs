using System;
using System.Linq.Expressions;
using System.Reflection;

namespace mambuquery.api.Infrastructure
{
    public class Between
    {
        public static Expression Build(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converted1 = Expression.Convert(constant1, member.Type);
            var converted2 = Expression.Convert(constant2, member.Type);


                var greaterThanComparison = Expression.GreaterThan(member, converted1);
                var lessThanComparison = Expression.LessThan(member, converted2);
                return Expression.And(greaterThanComparison, lessThanComparison);

        }
    }
}