using System;
using System.Linq.Expressions;
using System.Reflection;

namespace mambuquery.api.Infrastructure
{
    public class EqualsTo : BaseStringExpression
    {
        public static Expression Build(MemberExpression member, ConstantExpression constant)
        {
            var propertyType = ((PropertyInfo)member.Member).PropertyType;

            if (propertyType == typeof(string))
            {
                var values = NormalizeString(member, constant);
                return Expression.Equal(values.Item1, values.Item2);
            }

            return Expression.Equal(member, constant);

        }
    }
}