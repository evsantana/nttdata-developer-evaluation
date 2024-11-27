using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class CartTests
    {
        [Fact(DisplayName = "Create Cart with valid state")]
        public void CreateCart_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => CartTestData.GenerateValidCart();

            action.Should()
                .NotThrow<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Cart with empty list of items")]
        public void CreateCart_WithEmptyCartItemsParameters_ResultObjectInvalidState()
        {
            Action action = () => new Cart(Guid.NewGuid(), new List<CartItem>());

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Invalid list of items.");
        }
    }
}
