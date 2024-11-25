using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        /// <summary>
        /// Retrieves a paginated list with all entities
        /// </summary>
        /// <param name="currentPage">Page number to retrieve</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="orderDirection">Order Direction</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A paginated list of entities</returns>
        Task<PaginatedList<Cart>> GetPaginatedAsync(int currentPage, int pageSize, string orderBy, string orderDirection,CancellationToken cancellationToken);

    }
}
