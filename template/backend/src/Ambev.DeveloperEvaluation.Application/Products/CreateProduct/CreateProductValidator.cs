using Ambev.DeveloperEvaluation.Application.Products.Command;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(product => product.Name).NotEmpty().Length(3, 50);
            RuleFor(product => product.Price).NotEmpty().GreaterThan(0);
        }
    }
}
