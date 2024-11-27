using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact(DisplayName = "Create Sale with valid state")]
        public void CreateSale_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => SaleTestData.GenerateValidSale();

            action.Should()
                .NotThrow<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Sale with empty list of items")]
        public void CreateSale_WithEmptySaleItemsParameters_ResultObjectInvalidState()
        {

            Action action = () => new Sale("addd123", Guid.NewGuid(), 10, "String", new List<SaleItem>());

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Invalid list of items.");
        }

        [Fact(DisplayName = "Create sale with list of more than 20 identical items")]
        public void CreateSale_WithMore20ItemsIdenticalParameters_ResultObjectInvalidState()
        {
            Guid productId = Guid.NewGuid();

            Action action = () => new Sale("addd123", Guid.NewGuid(), 10, "String", 
                new List<SaleItem>()
                {
                    new SaleItem(productId, 20, 10, false),
                    new SaleItem(productId, 1, 10, false),
                    new SaleItem(Guid.NewGuid(), 10, 10, false)
                });

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Cannot sell more than 20 identical items.");
        }

        [Fact(DisplayName = "Create sale with invalid Sale Number parameter")]
        public void CreateSale_WithInvalidSaleNumberParameters_ResultObjectInvalidState()
        {
            Faker<Sale> SaleFaker = new Faker<Sale>()
                .CustomInstantiator(f => new Sale(
                    "a",
                    f.Random.Guid(),
                    f.Finance.Amount(1, 100),
                    f.Company.CompanyName(),
                    SaleTestData.GenerateValidSaleItem()
                 ));
           
            Action action = () => SaleFaker.Generate();

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Sale Number must be at least 2 characters long.");
        }

        [Fact(DisplayName = "Create sale with invalid Branch parameter")]
        public void CreateSale_WithInvalidBranchParameters_ResultObjectInvalidState()
        {
            Faker<Sale> SaleFaker = new Faker<Sale>()
                .CustomInstantiator(f => new Sale(
                    f.Random.String(10, 50),
                    f.Random.Guid(),
                    f.Finance.Amount(1, 100),
                    "a",
                    SaleTestData.GenerateValidSaleItem()
                 ));

            Action action = () => SaleFaker.Generate();

            action.Should()
                .Throw<DeveloperEvaluation.Domain.Exceptions.DomainExceptionValidation>()
                .WithMessage("Branch must be at least 2 characters long.");
        }
    }
}
