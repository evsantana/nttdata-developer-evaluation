using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Query
{
    public class GetCartByIdQuery : IRequest<Cart>
    {
        public Guid Id { get; set; }

        public GetCartByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
