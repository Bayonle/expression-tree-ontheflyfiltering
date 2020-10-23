using System;
using System.Linq.Expressions;
using System.Reflection;

namespace mambuquery.api.Infrastructure
{
    public class EndsWith : BaseStringExpression
    {
        public static Expression Build(MemberExpression member, ConstantExpression constant)
        {
            var endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
            var values = NormalizeString(member, constant);

            return Expression.Call(values.Item1, endsWithMethod, values.Item2);

        }
    }
}