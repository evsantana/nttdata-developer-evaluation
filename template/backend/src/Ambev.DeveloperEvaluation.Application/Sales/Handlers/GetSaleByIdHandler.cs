using Ambev.DeveloperEvaluation.Application.Sales.Query;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, Sale>
    {
        private readonly ISaleRepository _repository;

        public GetSaleByIdHandler(ISaleRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Sale> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
