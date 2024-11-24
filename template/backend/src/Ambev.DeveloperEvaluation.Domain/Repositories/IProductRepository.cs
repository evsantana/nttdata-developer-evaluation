using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {

        /// <summary>
        /// Retrieves a paginated, ordered list of entities in a category
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="orderDirection">Order Direction</param>
        /// <param name="filters">Filters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of categories paginated</returns>
        Task<PaginatedList<Product>> GetPaginateOrderedAsync(int currentPage, int pageSize, string orderBy, string orderDirection, IDictionary<string, string> filters, CancellationToken cancellationToken);


        /// <summary>
        /// Retrieves a paginated list of entities in a category
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="currentPage">Page number to retrieve</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>A paginated list of entities</returns>
        Task<PaginatedList<Product>> GetByCategoryPaginatedAsync(string categoryName, int currentPage, int pageSize, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve all product categories
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of categories</returns>
        Task<IEnumerable<string>> GetCategoriesAsync(CancellationToken cancellationToken);
    }
}
