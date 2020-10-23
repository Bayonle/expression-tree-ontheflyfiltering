using System;
using System.Linq.Expressions;
using System.Reflection;

namespace mambuquery.api.Infrastructure
{
    public class Contains : BaseStringExpression
    {
        public static Expression Build(MemberExpression member, ConstantExpression constant)
        {
            var containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
            var values = NormalizeString(member, constant);

            return Expression.Call(values.Item1, containsMethod, values.Item2);

        }
    }
}