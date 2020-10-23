using System;
using System.Linq.Expressions;
using System.Reflection;

namespace mambuquery.api.Infrastructure
{
    public class StartsWith : BaseStringExpression
    {
        public static Expression Build(MemberExpression member, ConstantExpression constant)
        {
            var startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            var values = NormalizeString(member, constant);

            return Expression.Call(values.Item1, startsWithMethod, values.Item2);

        }
    }
}