using Ambev.DeveloperEvaluation.Application.Carts.Command;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, DeleteResponse>
    {
        private readonly ICartRepository _repository;

        public DeleteCartHandler(ICartRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the DeleteCartCommand request
        /// </summary>
        /// <param name="request">The DeleteCartCommand command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>
        /// <exception cref="ApplicationException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<DeleteResponse> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (cart is null)
                throw new ApplicationException($"Cart could not be found.");

            var result = await _repository.DeleteAsync(cart, cancellationToken);

            if (result is null)
                throw new KeyNotFoundException("Cart not found.");

            return new DeleteResponse { Success = true };
        }
    }
}
