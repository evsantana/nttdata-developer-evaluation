using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities
{
    public sealed class CartItem : BaseEntity
    {
        #region Constructors
        public CartItem(Guid productId, int quantity, Guid cartId)
        {
            Validation(productId, quantity, cartId);
        }
        public CartItem(Guid id, Guid productId, int quantity, Guid cartId)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(id.ToString()), "Invalid ID value.");
            Validation(productId, quantity, cartId);
            Id = id;
        }
        #endregion

        #region Properties
        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        #endregion

        public void Update(Guid productId, int quantity, Guid cartid)
        {
            Validation(productId, quantity, cartid);
        }


        private void Validation(Guid productId, int quantity, Guid cartid)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(productId.ToString()), "Product ID is required.");

            DomainExceptionValidation.When(quantity <= 0, "Invalid quantity value.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(cartid.ToString()), "Cart ID is required.");

            ProductId = productId;
            Quantity = quantity;
            CartId = cartid;
        }
    }
}
