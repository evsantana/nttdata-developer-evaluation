using Ambev.DeveloperEvaluation.Application.Carts.Command;
using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, Cart>
    {
        private readonly ICartRepository _repository;

        public UpdateCartHandler(ICartRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Cart> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (cart is null)
                throw new ApplicationException($"Cart could not be found");

            cart.Update(request.UserId, request.CartItems);
            return await _repository.UpdateAsync(cart, cancellationToken);
        }
    }
}
