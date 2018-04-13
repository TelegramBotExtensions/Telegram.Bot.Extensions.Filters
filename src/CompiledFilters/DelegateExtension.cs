using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    internal static class DelegateExtension
    {
        public static MethodCallExpression GetMethodCall(this Delegate function, params Expression[] parameters)
        {
            return Expression.Call(
                function.Target == null ? null : Expression.Constant(function.Target),
                function.Method,
                parameters);
        }
    }
}
