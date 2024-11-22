using Ambev.DeveloperEvaluation.Application.Products.Query;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers
{
    public class GetProductByCategoryHandler : IRequestHandler<GetProductByCategoryQuery, PaginatedList<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByCategoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PaginatedList<Product>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByCategoryPaginatedAsync(request.CategoryName, request.CurrentPage, request.PageSize, cancellationToken);
        }
    }
}
