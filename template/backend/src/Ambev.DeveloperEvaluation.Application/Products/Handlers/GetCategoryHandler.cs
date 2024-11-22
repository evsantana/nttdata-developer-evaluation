using Ambev.DeveloperEvaluation.Application.Products.Query;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers
{
    public class GetCategoryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<string>>
    {
        private readonly IProductRepository _productRepository;

        public GetCategoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
        }

        /// <summary>
        /// Retrieve all categories
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of categories</returns>
        public async Task<IEnumerable<string>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetCategoriesAsync(cancellationToken);
        }
    }
}
