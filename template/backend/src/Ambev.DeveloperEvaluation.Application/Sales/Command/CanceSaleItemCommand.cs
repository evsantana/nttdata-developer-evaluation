using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Command
{
    public class CancelSaleItemCommand : IRequest<CancelResponse>
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }

        public CancelSaleItemCommand(Guid saleId, Guid itemId)
        {
            SaleId = saleId;
            ItemId = itemId;
        }
    }
}
