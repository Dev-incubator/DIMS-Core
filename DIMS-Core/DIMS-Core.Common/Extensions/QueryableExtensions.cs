using DIMS_Core.Common.Enums;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DIMS_Core.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> DynamicSort<T, TKey>(this IQueryable<T> query, 
            string sortExpression, 
            Expression<Func<T,TKey>> defaultSorting,
            SortDirections defaultDirection)
        {
            if(sortExpression.IsNullOrWhiteSpace() && defaultSorting != null)
            {
                switch (defaultDirection)
                {
                    case SortDirections.DESC:
                        query = query.OrderByDescending(defaultSorting);
                        break;
                    case SortDirections.ASC:
                    default:
                        query = query.OrderBy(defaultSorting);
                        break;
                }

            }
            else
            {
                query = query.OrderBy(sortExpression);
            }

            return query;
        }

        public static IQueryable<T> SkipAndTake<T>(this IQueryable<T> query, 
            int page, 
            int pageSize)
        {
            if (pageSize > 0)
            {
                query = query.Skip(pageSize * page)
                    .Take(pageSize);
            }

            return query;
        }

        public static IQueryable<T> SortAndTake<T, TKey>(this IQueryable<T> query, 
            string sortExpression, 
            int page,
            int pageSize, 
            Expression<Func<T, TKey>> defaultSorting,
            SortDirections defaultDirection)
        {
            return query.DynamicSort(sortExpression, defaultSorting, defaultDirection)
                .SkipAndTake(page, pageSize);
        }
    }
}