using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.Command;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteResponse>
    {
        private readonly ISaleRepository _repository;

        public DeleteSaleHandler(ISaleRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the DeleteSaleCommand request
        /// </summary>
        /// <param name="request">The DeleteSaleCommand command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>
        /// <exception cref="ApplicationException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<DeleteResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (sale is null)
                throw new ApplicationException($"Sale could not be found.");

            var result = await _repository.DeleteAsync(sale, cancellationToken);

            if (result is null)
                throw new KeyNotFoundException("Sale not found.");

            Log.Information("Sale deleted: {@SaleEvent}", result);

            return new DeleteResponse { Success = true };
        }
    }
}
