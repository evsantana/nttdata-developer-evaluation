using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities
{
    public sealed class SaleItem : BaseEntity
    {
        #region Constructors
        public SaleItem(Guid productId, int quantity, decimal unitPrice, bool isCancelled)
        {
            Validation(productId, quantity, unitPrice);
            IsCancelled = isCancelled;
            CalculateDiscount();
        }
        public SaleItem(Guid id, Guid productId, int quantity, decimal unitPrice, bool isCancelled)
        {
            Validation(productId, quantity, unitPrice);
            IsCancelled = isCancelled;
            CalculateDiscount();
            Id = id;
        }
        #endregion

        #region Properties
        public Guid ProductId { get; set; }
        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public bool IsCancelled { get; private set; }
        #endregion

        #region Methods
        public void Update(Guid productId, int quantity, decimal unitPrice, bool isCancelled)
        {
            Validation(productId, quantity, unitPrice);
            CalculateDiscount();
            Id = Id;
            IsCancelled = isCancelled;
        }

        public void Cancel(bool isCancelled)
        {
            IsCancelled = isCancelled;
        }

        private void ApplyDiscount(decimal discountPercentage)
        {
            Discount = discountPercentage;
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = Quantity * UnitPrice * (1 - Discount);
        }

        private void CalculateDiscount()
        {
            if (Quantity >= 4 && Quantity < 10)
                ApplyDiscount(0.10m);
            else if (Quantity >= 10 && Quantity <= 20)
                ApplyDiscount(0.20m);
            else
                ApplyDiscount(0.00m);

            CalculateTotalPrice();
        }

        private void Validation(Guid productId, int quantity, decimal unitPrice)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(productId.ToString()), "Product ID is required.");

            DomainExceptionValidation.When(quantity <= 0, "The 'quantity' field must be a positive number.");
            DomainExceptionValidation.When(quantity > 20, "Cannot sell more than 20 identical items.");

            DomainExceptionValidation.When(unitPrice <= 0, "The 'unit price' field must be a positive number.");

            UnitPrice = unitPrice;
            Quantity = quantity;
            ProductId = productId;
        }
        #endregion
    }
}
