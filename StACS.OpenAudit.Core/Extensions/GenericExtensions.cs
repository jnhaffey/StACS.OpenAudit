using System;
using System.Linq.Expressions;

namespace StACS.OpenAudit.Core.Extensions
{
    public static class GenericExtensions
    {
        public static T GetDefaultValue<T>(this T source)
        {
            var e = Expression.Lambda<Func<T>>(Expression.Default(typeof(T)));
            return e.Compile()();
        }
    }
}