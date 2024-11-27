using Ambev.DeveloperEvaluation.Application.Sales.Command;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public class CreateSaleHandlerTestData
    {

        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .CustomInstantiator(s => new SaleItem(
            s.Random.Guid(),
            s.Random.Number(1, 20),
            s.Finance.Amount(1, 100),
            false
         ));

        private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
            .RuleFor(u => u.UserId, f => f.Random.Guid())
            .RuleFor(u => u.SaleNumber, f => f.Random.String(10, 50))
            .RuleFor(u => u.SaleDate, f => f.Date.Future())
            .RuleFor(u => u.TotalAmount, f => f.Finance.Amount(1, 100))
            .RuleFor(u => u.Branch, f => f.Company.CompanyName())
            .RuleFor(u => u.SaleItems, SaleItemFaker.Generate(2))
            ;




        public static CreateSaleCommand GenerateValidCommand()
        {
            return createSaleHandlerFaker.Generate();
        }
    }
}
