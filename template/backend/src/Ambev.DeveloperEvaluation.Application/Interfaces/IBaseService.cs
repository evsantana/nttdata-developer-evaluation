using Ambev.DeveloperEvaluation.Common.Pagination;

namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<PaginatedList<T>> GetPaginatedAsync(int currentPage, int pageSize,string orderBy, string orderDirection, CancellationToken cancellationToken);
    }
}
