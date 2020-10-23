using System;
using System.Linq.Expressions;
using System.Reflection;

namespace mambuquery.api.Infrastructure
{
    public class Empty
    {
        public static Expression Build(MemberExpression member, ConstantExpression constant)
        {
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var nullConstant = Expression.Constant(null);
            var emptyConstant = Expression.Constant(string.Empty);

            if (propertyType == typeof(string))
            {
                // var methodCall = Expression.Call(typeof(string), nameof(string.IsNullOrWhiteSpace), null, member);
                // var nullOrWhiteSpaceComparison = Expression.Not(methodCall);
                // return nullOrWhiteSpaceComparison;
                var nullComparison = Expression.Equal(member, nullConstant);
                var emptyComparison = Expression.Equal(member, emptyConstant);
                return Expression.Or(nullComparison, emptyComparison);
            }

            else
                return Expression.Equal(member, nullConstant);

        }
    }
}