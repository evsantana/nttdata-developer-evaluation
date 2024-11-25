using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Command
{
    public class DeleteCartCommand : IRequest<DeleteResponse>
    {
        public Guid Id { get; set; }
        public DeleteCartCommand(Guid id)
        {
            Id = id;
        }
    }
}
