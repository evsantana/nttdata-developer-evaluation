using Ambev.DeveloperEvaluation.Application.Carts.Query;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers
{
    public class GetCartHandler : IRequestHandler<GetCartQuery, PaginatedList<Cart>>
    {
        private readonly ICartRepository _repository;

        public GetCartHandler(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetPaginatedAsync(request.CurrentPage, request.PageSize, request.OrderBy, request.OrderDirection, cancellationToken);
        }
    }
}
