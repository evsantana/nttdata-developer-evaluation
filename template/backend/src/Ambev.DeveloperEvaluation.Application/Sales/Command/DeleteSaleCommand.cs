using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Command
{
    public class DeleteSaleCommand : IRequest<DeleteResponse>
    {
        public Guid Id { get; set; }
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
