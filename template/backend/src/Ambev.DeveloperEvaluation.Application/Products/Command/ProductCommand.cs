using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Command
{
    /// <summary>
    /// Abstract class for the base of commands
    /// </summary>
    public abstract class ProductCommand : IRequest<Product>
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public double Rating { get; set; }

        public int Count { get; set; }


    }
}
