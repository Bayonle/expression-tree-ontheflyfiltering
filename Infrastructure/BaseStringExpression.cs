using System;
using System.Linq.Expressions;
using System.Reflection;

namespace mambuquery.api.Infrastructure
{
    public class BaseStringExpression
    {
        public static (MethodCallExpression, MethodCallExpression) NormalizeString(MemberExpression member, ConstantExpression constant)
        {
            var toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);
            var trimMethod = typeof(string).GetMethod("Trim", new Type[0]);

            var trimMemberCall = Expression.Call(member, trimMethod);
            // x.Name.Trim().ToLower()
            var left = Expression.Call(trimMemberCall, toLowerMethod);
            // "John ".Trim()
            var trimConstantCall = Expression.Call(constant, trimMethod);
            // "John ".Trim().ToLower()
            var right = Expression.Call(trimConstantCall, toLowerMethod);

            return (left, right);

        }

    }
}