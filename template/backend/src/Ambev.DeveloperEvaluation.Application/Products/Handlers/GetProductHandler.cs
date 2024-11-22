using Ambev.DeveloperEvaluation.Application.Products.Query;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers
{
    public class GetProductHandler : IRequestHandler<GetProductsQuery, PaginatedList<Product>>
    {

        private readonly IProductRepository _productRepository;

        public GetProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<PaginatedList<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetPaginateOrderedAsync(request.CurrentPage, request.PageSize, request.OrderBy, request.OrderDirection, cancellationToken);
        }
    }
}
