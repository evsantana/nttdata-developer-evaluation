using Ambev.DeveloperEvaluation.Application.Sales.Command;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, Sale>
    {
        private readonly ISaleRepository _repository;

        public UpdateSaleHandler(ISaleRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Sale> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null)
                throw new ApplicationException($"Sale could not be found");

            sale.Update(request.SaleNumber, request.UserId, request.TotalAmount, request.Branch, request.SaleItems);

            Log.Information("Sale updated: {@SaleEvent}", sale);

            return await _repository.UpdateAsync(sale, cancellationToken);
        }
    }
}
