using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Models.UserCase.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities
{
    public sealed class Sale : BaseEntity
    {

        #region Constructors
        public Sale(string saleNumber, Guid userId, decimal totalAmount, string branch, ICollection<SaleItem> saleItems)
        {
            DomainExceptionValidation.When(saleItems.Count < 1, "Invalid list of items.");

            Validation(saleNumber, branch, userId);
            UpdateItems(saleItems);
            CalculateTotal();

        }
        public Sale(Guid id, string saleNumber, Guid userId, decimal totalAmount, string branch)
        {
            Validation(saleNumber, branch, userId);
            TotalAmount = totalAmount;
            Id = id;
        }
        #endregion

        #region Properties

        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal TotalAmount { get; private set; }
        public string Branch { get; private set; }
        public ICollection<SaleItem> SaleItems { get; set; }
        public bool IsCanceled { get; private set; }
        #endregion

        #region Methods
        public void CalculateTotal()
        {
            //Calculates the total value of items that are not in canceled status
            TotalAmount = SaleItems.Count > 0 ? SaleItems.Where(p => !IsCanceled).Sum(item => item.TotalPrice) : 0;
        }

        private void UpdateItems(ICollection<SaleItem> saleItems)
        {
            //Groups duplicate items
            SaleItems = saleItems
                .GroupBy(item => item.ProductId) //Group items by ProductId
                .Select(group => new SaleItem(
                    group.Key,  // productId
                    group.Sum(item => item.Quantity),  // Sum quantity
                    group.First().UnitPrice, //Unit Price can be taken from the first item in the group
                    group.First().IsCancelled
                ))
                .ToList();
        }

        public void Update(string saleNumber, Guid userId, decimal totalAmount, string branch, ICollection<SaleItem> saleItems)
        {
            Validation(saleNumber, branch, userId);
            UpdateItems(saleItems);
            CalculateTotal();
            SaleItems = saleItems;
        }

        public void Cancel(bool isCancelled)
        {
            IsCanceled = isCancelled;
        }

        private void Validation(string saleNumber, string branch, Guid userId)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(saleNumber), "Sale Number is required.");
            DomainExceptionValidation.When(saleNumber.Length < 2, "Sale Number must be at least 2 characters long.");
            DomainExceptionValidation.When(saleNumber.Length > 50, "Sale Number cannot be longer than 50 characters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(userId.ToString()), "User ID is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(branch), "Branch is required.");
            DomainExceptionValidation.When(branch.Length < 2, "Branch must be at least 2 characters long.");
            DomainExceptionValidation.When(branch.Length > 150, "Branch cannot be longer than 50 characters.");

            SaleNumber = saleNumber;
            Branch = branch;
            UserId = userId;
        }
        #endregion

    }
}
