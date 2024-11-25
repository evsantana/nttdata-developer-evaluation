using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM.Extensions
{
    public static class SortExtension
    {
        /// <summary>
        /// Create query order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Query</param>
        /// <param name="orderBy">Field</param>
        /// <param name="orderDirection">Direction</param>
        /// <returns>Ordered query</returns>
        public static IQueryable<T>? SetSort<T>(this IQueryable<T> query, string orderBy, string orderDirection)
        {
            string direction = orderDirection.Trim().ToLower();

            //Order direction
            if (direction != "desc" && direction != "asc")
                return query;

            //Field exists
            var property = typeof(T).GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null) return query;

            //Parameter for dynamic expression (eg.: "x")
            var parameter = Expression.Parameter(typeof(T), "x");
            //Property (eg.: "x.PropertyName")
            var member = Expression.Property(parameter, orderBy);
            // Lambda expression (eg.: "x => x.PropertyName")
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(member, typeof(object)), parameter);

            return direction == "asc"
                ? query.OrderBy(lambda)
                : query.OrderByDescending(lambda);

        }
    }

}
