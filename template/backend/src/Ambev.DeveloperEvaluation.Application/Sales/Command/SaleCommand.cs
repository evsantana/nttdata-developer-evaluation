using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using MediatR;


namespace Ambev.DeveloperEvaluation.Application.Sales.Command
{
    public abstract class SaleCommand : IRequest<Sale>
    {
        public Guid UserId { get; set; }

        public string SaleNumber { get; set; }

        public DateTime SaleDate { get;  set; }

        public decimal TotalAmount { get; set; }

        public string Branch { get; set; }

        public List<SaleItem> SaleItems { get; set; }
    }
}
