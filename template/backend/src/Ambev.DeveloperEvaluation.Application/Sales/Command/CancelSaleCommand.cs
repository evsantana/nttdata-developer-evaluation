using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Command
{
    public class CancelSaleCommand : IRequest<CancelResponse>
    {
        public Guid Id { get; set; }
        public CancelSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
