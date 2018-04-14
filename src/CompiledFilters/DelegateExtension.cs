using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    internal static class DelegateExtension
    {
        public static Expression GetInvocation(this Delegate function, Expression parameter)
        {
            return Expression.Invoke(Expression.Constant(function), parameter);
        }
    }
}
