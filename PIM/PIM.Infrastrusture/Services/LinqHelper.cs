using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
