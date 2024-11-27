using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class CartItemTests
    {
        [Fact(DisplayName = "Create Cart Item with valid state")]
        public void CreateCartItem_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => CartTestData.GenerateValidCartItem();

            action.Should()
                .NotThrow<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Cart Item with invalid quantity parameter")]
        public void CreateCartItem_WithInvalidQuantityParameters_ResultObjectInvalidState()
        {
            Action action = () => new CartItem(new Guid(), 0, new Guid());

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Invalid quantity value.");
        }
    }
}
