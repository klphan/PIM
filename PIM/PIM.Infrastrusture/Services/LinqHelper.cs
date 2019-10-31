using System;
using System.Linq;
using System.Linq.Expressions;

namespace PIM.Infrastructure.Services
{
    public static class LinqHelper
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            bool descending) =>
        descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }
}
