using Ambev.DeveloperEvaluation.Application.Sales.Query;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class GetSaleHandler : IRequestHandler<GetSaleQuery, PaginatedList<Sale>>
    {
        private readonly ISaleRepository _repository;

        public GetSaleHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<Sale>> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetPaginatedAsync(request.CurrentPage, request.PageSize, request.OrderBy, request.OrderDirection, cancellationToken);
        }
    }
}
