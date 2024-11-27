using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface ISaleService : IBaseService<SaleDTO>
    {
        Task CancelAsync(Guid id, CancellationToken cancellationToken);
        Task CancelItemAsync(Guid id, Guid itemId, CancellationToken cancellationToken);
    }
}
