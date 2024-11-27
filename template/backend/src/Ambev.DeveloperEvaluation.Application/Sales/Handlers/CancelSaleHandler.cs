using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.Command;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelResponse>
    {
        private readonly ISaleRepository _repository;

        public CancelSaleHandler(ISaleRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

        }

        public async Task<CancelResponse> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null)
                throw new ApplicationException($"Sale could not be found");

            sale.Cancel(true);

            var result = await _repository.UpdateAsync(sale, cancellationToken);

            if (result is null)
                throw new KeyNotFoundException("Sale not cancelled.");

            Log.Information("Sale cancelled: {@SaleEvent}", sale);

            return new CancelResponse { Success = true };

        }
    }
}
