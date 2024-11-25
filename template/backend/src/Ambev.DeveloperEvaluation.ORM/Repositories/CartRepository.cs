using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        #region Properties and Constructors
        private readonly DefaultContext _context;

        public CartRepository(DefaultContext context) : base(context)
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
        public async Task<Cart?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Set<Cart>()
                .Include(cart => cart.CartItems)
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
        public async Task<PaginatedList<Cart>> GetPaginatedAsync(int currentPage, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken)
        {
            var query = _context.Set<Cart>()
                .Include(cart => cart.CartItems)
                .AsNoTracking();

            query = query.SetSort(orderBy, orderDirection);

            return await PaginatedList<Cart>.CreateAsync(query, currentPage, pageSize);
        }
    }
}
