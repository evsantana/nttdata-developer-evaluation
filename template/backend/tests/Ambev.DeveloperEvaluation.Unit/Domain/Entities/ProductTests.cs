using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class ProductTests
    {
        [Fact(DisplayName = "Create a Product with valid state")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => ProductTestData.GenerateValidProduct();

            action.Should()
                .NotThrow<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create a Product with invalid Title parameter")]
        public void CreateProduct_WithInvalidTitleParameter_ResultObjectInvalidState()
        {
            var product = new Faker<Product>()
            .CustomInstantiator(s => new Product(
                "a",
                s.Finance.Amount(1, 100),
                s.Random.String(1, 250),
                s.Random.String(1, 250),
                s.Random.String(1, 250),
                s.Random.Double(),
                s.Random.Int(100)
             ));
            Action action = () => product.Generate();

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Title must be at least 2 characters long.");
        }

        [Fact(DisplayName = "Create a Product with invalid Price parameter")]
        public void CreateProduct_WithInvalidPriceParameter_ResultObjectInvalidState()
        {
            var product = new Faker<Product>()
            .CustomInstantiator(s => new Product(
                s.Random.String(10, 50),
                0,
                s.Random.String(1, 250),
                s.Random.String(1, 250),
                s.Random.String(1, 250),
                s.Random.Double(),
                s.Random.Int(100)
             ));
            Action action = () => product.Generate();

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("The 'price' field must be a positive number.");
        }

        [Fact(DisplayName = "Create a Product with invalid Description parameter")]
        public void CreateProduct_WithInvalidDescriptionParameter_ResultObjectInvalidState()
        {
            var product = new Faker<Product>()
            .CustomInstantiator(s => new Product(
                s.Random.String(10, 50),
                s.Finance.Amount(1, 100),
                "a",
                s.Random.String(1, 250),
                s.Random.String(1, 250),
                s.Random.Double(),
                s.Random.Int(100)
             ));
            Action action = () => product.Generate();

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Description must be at least 2 characters long.");
        }

        [Fact(DisplayName = "Create a Product with invalid Category parameter")]
        public void CreateProduct_WithInvalidCategoryParameter_ResultObjectInvalidState()
        {
            var product = new Faker<Product>()
            .CustomInstantiator(s => new Product(
                s.Random.String(10, 50),
                s.Finance.Amount(1, 100),
                s.Random.String(1, 250),
                "a",
                s.Random.String(1, 250),
                s.Random.Double(),
                s.Random.Int(100)
             ));
            Action action = () => product.Generate();

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Category must be at least 2 characters long.");
        }

        [Fact(DisplayName = "Create a Product with invalid Image parameter")]
        public void CreateProduct_WithInvalidImageParameter_ResultObjectInvalidState()
        {
            var product = new Faker<Product>()
            .CustomInstantiator(s => new Product(
                s.Random.String(10, 50),
                s.Finance.Amount(1, 100),
                s.Random.String(1, 250),
                s.Random.String(1, 250),
                s.Random.String(250, 350),
                s.Random.Double(),
                s.Random.Int(100)
             ));
            Action action = () => product.Generate();

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Image cannot be longer than 250 characters.");
        }
    }
}
