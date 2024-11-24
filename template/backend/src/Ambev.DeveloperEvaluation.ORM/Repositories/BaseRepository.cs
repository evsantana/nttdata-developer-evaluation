using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of IBaseRepository using Entity Framework Core
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region Properties and Constructors
        protected readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of BaseRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public BaseRepository(DefaultContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// Creates a new entity in the repository
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created entity</returns>
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The deleted entity</returns>
        public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Retrieves a list with all entities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves an entity by their unique identifier
        /// </summary>
        /// <param name="Id">The unique identifier of the entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity if found, null otherwise</returns>
        public async Task<T?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated entity.</returns>
        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Retrieves a paginated list with all entities.
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public async Task<PaginatedList<T>> GetPaginatedAsync(int currentPage, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Set<T>().AsNoTracking();

            return await PaginatedList<T>.CreateAsync(query, currentPage, pageSize);
        }


    }
}
