using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleTestData
    {
        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
            .CustomInstantiator(s => new SaleItem(
                s.Random.Guid(),
                s.Random.Number(1, 20),
                s.Finance.Amount(1, 100),
                false
             ));

        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
            .CustomInstantiator(f => new Sale(
                f.Random.String(10, 50),
                f.Random.Guid(),
                f.Finance.Amount(1, 100),
                f.Company.CompanyName(),
                SaleItemFaker.Generate(1)
             ));

        private static readonly Faker<SaleItem> SaleItemInvalidFaker = new Faker<SaleItem>()
            .CustomInstantiator(s => new SaleItem(
                s.Random.Guid(),
                0,
                0,
                false
             ));

        /// <summary>
        /// Generates a valid Sale entity with randomized data.
        /// The generated sale will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <param name="itemCount">The number of items to generate</param>
        /// <returns>A valid Sale entity with randomly generated data.</returns>
        public static Sale GenerateValidSale(int itemCount = 1)
        {
            var saleItems = SaleItemFaker.Generate(itemCount);
            var sale = SaleFaker.Generate();
            sale.SaleItems = saleItems;

            return sale;
        }

        /// <summary>
        /// Generates a valid Sale Item entity with randomized data.
        /// </summary>
        public static List<SaleItem> GenerateValidSaleItem(int itemCount = 1)
        {
            var saleItem = SaleItemFaker.Generate(itemCount);
            return saleItem;
        }

        /// <summary>
        /// Generates a valid list of SaleItem entities with randomized data.
        /// </summary>
        /// <param name="itemCount">The number of items to generate</param>
        /// <returns>A list of valid SaleItem entities.</returns>
        public static List<SaleItem> GenerateValidSaleItems(int itemCount = 1)
        {
            return SaleItemFaker.Generate(itemCount);
        }

        /// <summary>
        /// Generates an invalid Sale entity for testing negative scenarios.
        /// </summary>
        /// <returns>An invalid Sale entity.</returns>
        public static Sale GenerateInvalidSale()
        {
            var saleItems = SaleItemInvalidFaker.Generate(1);

            var sale = new Sale(
                "",
                SaleFaker.Generate().UserId,
                0,
                "",
                saleItems
            );

            return sale;
        }
    }
}
