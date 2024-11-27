using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Models.UserCase.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities
{
    public sealed class Cart : BaseEntity
    {
        #region Constructors
        public Cart() { }
        public Cart(Guid userId, ICollection<CartItem> cartItems)
        {
            DomainExceptionValidation.When(cartItems.Count <= 0, "Invalid list of items.");
            Validation(userId);
            CartItems = cartItems;
        }

        public Cart(Guid id, Guid userId)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(id.ToString()), "Invalid ID value.");
            Validation(userId);
            Id = id;
        }
        #endregion

        #region Properties
        public Guid UserId { get; private set; }

        public User User { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        #endregion

        public void Update(Guid userId, ICollection<CartItem> cartItems)
        {
            Validation(userId);
            CartItems = cartItems;
        }

        private void Validation(Guid userId)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(userId.ToString()), "User ID is required.");
            UserId = userId;
        }
    }
}
