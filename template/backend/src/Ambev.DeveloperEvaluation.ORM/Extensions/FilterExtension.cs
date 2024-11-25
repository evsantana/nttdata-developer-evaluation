using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM.Extensions
{
    public static class FilterExtension
    {
        /// <summary>
        /// Create query filters
        /// </summary>
        /// <typeparam name="T">Type object</typeparam>
        /// <param name="query">Linq query</param>
        /// <param name="filters">Filters</param>
        /// <returns>Filtered query</returns>
        public static IQueryable<T> SetFilters<T>(this IQueryable<T> query, IDictionary<string, string> filters)
        {
            if (filters is null)
                return query;

            foreach (var f in filters)
            {
                var propertyName = f.Key;
                var value = f.Value;
                bool minRange = propertyName.StartsWith("_min");
                bool maxRange = propertyName.StartsWith("_max");
                bool wildcard = value.StartsWith("*") || value.EndsWith("*");

                if (minRange || maxRange)
                    propertyName = propertyName.Substring(4); //Remove _min or _max

                //Field exists
                var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null) continue;

                //Lambda expression
                Expression<Func<T, bool>> lambda = null;

                //Parameter for dynamic expression
                var parameter = Expression.Parameter(typeof(T), "x");

                if (minRange || maxRange)
                {
                    lambda = GetRangeFilter<T>(parameter, property, value, minRange);
                }
                else if (wildcard && property.PropertyType == typeof(string))
                {
                    lambda = GetStringOrExactFilter<T>(parameter, property, value);
                }
                else
                {
                    //Dynamic expression
                    var member = Expression.Property(parameter, propertyName);
                    var constant = Expression.Constant(Convert.ChangeType(value, property.PropertyType));
                    var comparison = Expression.Equal(member, constant);
                    lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                }

                //Set filter
                if (lambda != null)
                    query = query.Where(lambda);

            }

            return query;
        }

        private static Expression<Func<T, bool>> GetStringOrExactFilter<T>(ParameterExpression parameter, PropertyInfo property, string value)
        {
            var member = Expression.Property(parameter, property.Name);

            if (value.StartsWith("*") && value.EndsWith("*"))
            {
                var containsValue = value.Trim('*');
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var constant = Expression.Constant(containsValue);
                var containsExpression = Expression.Call(member, method, constant);
                return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);

            }
            else if (value.StartsWith("*"))
            {
                // Termina com (*value)
                var endsWithValue = value.TrimStart('*');
                var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                var constant = Expression.Constant(endsWithValue);
                var endsWithExpression = Expression.Call(member, method, constant);
                return Expression.Lambda<Func<T, bool>>(endsWithExpression, parameter);
            }
            else if (value.EndsWith("*"))
            {
                // Começa com (value*)
                var startsWithValue = value.TrimEnd('*');
                var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                var constant = Expression.Constant(startsWithValue);
                var startsWithExpression = Expression.Call(member, method, constant);
                return Expression.Lambda<Func<T, bool>>(startsWithExpression, parameter);
            }


            return null;
        }

        private static Expression<Func<T, bool>> GetRangeFilter<T>(ParameterExpression parameter, PropertyInfo property, string value, bool isMin)
        {
            // Converter o valor para o tipo correto
            var propertyType = property.PropertyType;
            object convertedValue;

            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                //DateTime parsedDate = DateTime.Parse(value);
                convertedValue = DateTime.Parse(value).ToUniversalTime();

                //convertedValue = Convert.ChangeType(parsedDate, propertyType);
            }
            else if (propertyType == typeof(DateTimeOffset) || propertyType == typeof(DateTimeOffset?))
            {
                convertedValue = DateTimeOffset.Parse(value).ToUniversalTime();
            }
            else
            {
                convertedValue = Convert.ChangeType(value, propertyType);
            }


            // Criar a expressão de comparação
            var member = Expression.Property(parameter, property.Name);
            var constant = Expression.Constant(convertedValue, propertyType);

            var comparison = isMin
                ? Expression.GreaterThanOrEqual(member, constant)
                : Expression.LessThanOrEqual(member, constant);

            return Expression.Lambda<Func<T, bool>>(comparison, parameter);
        }


    }
}
