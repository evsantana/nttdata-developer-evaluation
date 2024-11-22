using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Query
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid Id { get; set; }
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
