using Ambev.DeveloperEvaluation.Application.Carts.Query;
using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers
{
    public class GetCartByIdHandler : IRequestHandler<GetCartByIdQuery, Cart>
    {
        private readonly ICartRepository _repository;

        public GetCartByIdHandler(ICartRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Cart> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
