using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        #region Properties and Constructors
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// Retrieves a paginated, ordered list of entities in a category
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="orderDirection">Order Direction</param>
        /// <param name="filters">Filter</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of categories paginated</returns>
        public async Task<PaginatedList<Product>> GetPaginateOrderedAsync(int currentPage, int pageSize, string orderBy, string orderDirection, IDictionary<string, string> filters, CancellationToken cancellationToken)
        {
            var query = _context.Products
                .AsNoTracking();

            query = query.SetFilters(filters);
            query = query.SetSort(orderBy, orderDirection);

            return await PaginatedList<Product>.CreateAsync(query, currentPage, pageSize);
        }

        /// <summary>
        /// Retrieve all product categories
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of categories</returns>
        public async Task<IEnumerable<string>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(c => c.Category != null)
                .Select(c => c.Category)
                .Distinct()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves a paginated list of entities in a category
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="currentPage">Page number to retrieve</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>A paginated list of entities</returns>
        public async Task<PaginatedList<Product>> GetByCategoryPaginatedAsync(string categoryName, int currentPage, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Products
                .AsNoTracking()
                .Where(p => p.Category != null && p.Category == categoryName).Distinct();

            return await PaginatedList<Product>.CreateAsync(query, currentPage, pageSize);
        }
    }
}
