using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Query
{
    public class GetSaleByIdQuery : IRequest<Sale>
    {
        public Guid Id { get; set; }

        public GetSaleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
