using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        #region Properties and Constructors
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// Retrieves an entity by their unique identifier
        /// </summary>
        /// <param name="Id">The unique identifier of the entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity if found, null otherwise</returns>
        public async Task<Sale?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Set<Sale>()
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a paginated list with all entities.
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="orderDirection">Order Direction</param>
        /// <param name="filters">Filter</param>
        /// <returns></returns>
        public async Task<PaginatedList<Sale>> GetPaginatedAsync(int currentPage, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken)
        {
            var query = _context.Set<Sale>()
                .Include(s => s.SaleItems)
                .AsNoTracking();

            query = query.SetSort(orderBy, orderDirection);

            return await PaginatedList<Sale>.CreateAsync(query, currentPage, pageSize);
        }
    }
}
