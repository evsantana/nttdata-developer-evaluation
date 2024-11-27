using Ambev.DeveloperEvaluation.Application.Sales.Command;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Sale>
    {
        private readonly ISaleRepository _repository;

        public CreateSaleHandler(ISaleRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

        }

        /// <summary>
        /// Handles the CreateSaleCommand request
        /// </summary>
        /// <param name="request">The CreateSaleCommand command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the create operation</returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<Sale> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale(request.SaleNumber, request.UserId, request.TotalAmount, request.Branch, request.SaleItems);
            if (sale is null)
                throw new ApplicationException($"Error creating entity");

            var _result = await _repository.CreateAsync(sale, cancellationToken);

            Log.Information("Sale created: {@SaleEvent}", _result);

            return _result;
        }
    }
}
