using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleItemTests
    {
        [Fact(DisplayName = "Create Sale Item with valid state")]
        public void CreateSaleItem_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => SaleTestData.GenerateValidSaleItem();

            action.Should()
                .NotThrow<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create sale item with valid state and 10% discount")]
        public void Given_TotalPrice_When_10PercentDiscount_Then_TotalPriceShouldCorretly()
        {
            // Arrange
            var saleItem = new SaleItem(new Guid(), 5, 10, false);

            // Act
            var totalPrice = saleItem.TotalPrice;

            // Assert
            totalPrice.Should()
                .Be(45);
        }

        [Fact(DisplayName = "Create sale item with valid state and 20% discount")]
        public void Given_TotalPrice_When_20PercentDiscount_Then_TotalPriceShouldCorretly()
        {
            // Arrange
            var saleItem = new SaleItem(new Guid(), 10, 10, false);

            // Act
            var totalPrice = saleItem.TotalPrice;

            // Assert
            totalPrice.Should().Be(80);
        }

        [Fact(DisplayName = "Create Sale Item with invalid quantity parameter")]
        public void CreateSaleItem_WithInvalidQuantityParameters_ResultObjectInvalidState()
        {
            Action action = () => new SaleItem(new Guid(), 0, 10, false);

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("The 'quantity' field must be a positive number.");
        }

        [Fact(DisplayName = "Create Sale Item with invalid unit price parameter")]
        public void CreateSaleItem_WithInvalidUnitPriceParameters_ResultObjectInvalidState()
        {
            Action action = () => new SaleItem(new Guid(), 10, 0, false);

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("The 'unit price' field must be a positive number.");
        }

        [Fact(DisplayName = "Create sales item with quantity parameter above 20")]
        public void CreateSaleItem_WithQuantityAbove20Parameters_ResultObjectInvalidState()
        {
            Action action = () => new SaleItem(new Guid(), 21, 10, false);

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Cannot sell more than 20 identical items.");
        }



    }
}
