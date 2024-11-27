using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.Command;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelResponse>
    {
        private readonly ISaleRepository _repository;
        private readonly ISaleItemRepository _itemRepository;

        public CancelSaleItemHandler(ISaleRepository repository, ISaleItemRepository itemRepository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _itemRepository = itemRepository ??
                throw new ArgumentNullException(nameof(itemRepository));

        }

        public async Task<CancelResponse> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale is null)
                throw new ApplicationException($"Sale could not be found");

            var saleItem = sale.SaleItems.FirstOrDefault(si => si.ProductId == request.ItemId);

            if (saleItem is null)
                throw new ApplicationException($"Product could not be found");

            saleItem.Cancel(true);

            var result = await _itemRepository.UpdateAsync(saleItem, cancellationToken);

            if (result is null)
                throw new KeyNotFoundException("Product not cancelled.");

            Log.Information("Sale Product Item cancelled: {@SaleEvent}", result);

            return new CancelResponse { Success = true };
        }
    }
}
