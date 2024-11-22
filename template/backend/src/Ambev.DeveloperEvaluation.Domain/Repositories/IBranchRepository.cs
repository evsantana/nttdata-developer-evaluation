using Ambev.DeveloperEvaluation.Domain.Models.BranchCase.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IBranchRepository : IBaseRepository<Branch>
    {
        //Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        //Task<IEnumerable<Branch>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
