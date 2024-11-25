using Ambev.DeveloperEvaluation.Application.Carts.Command;
using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, Cart>
    {
        private readonly ICartRepository _repository;

        public CreateCartHandler(ICartRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

        }

        /// <summary>
        /// Handles the CreateCartCommand request
        /// </summary>
        /// <param name="request">The CreateCartCommand command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the create operation</returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<Cart> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart(request.UserId, request.CartItems);
            if (cart is null)
                throw new ApplicationException($"Error creating entity");

            return await _repository.CreateAsync(cart, cancellationToken);
        }
    }
}
