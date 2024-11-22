using Ambev.DeveloperEvaluation.Application.Products.Command;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));

        }

        /// <summary>
        /// Handles the CreateProductCommand request
        /// </summary>
        /// <param name="request">The CreateProductCommand command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the create operation</returns>
        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Title, request.Price, request.Description, request.Category, request.Image, request.Rating, request.Count);
            if (product is null)
                throw new ApplicationException($"Error creating entity");

            return await _productRepository.CreateAsync(product, cancellationToken);
        }
    }
}
