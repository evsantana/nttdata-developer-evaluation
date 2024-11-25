using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Command
{
    public abstract class CartCommand : IRequest<Cart>
    {
        public Guid UserId { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}
