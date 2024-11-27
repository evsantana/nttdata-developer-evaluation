using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class ProductTestData
    {
        private static readonly Faker<Product> ProductItemFaker = new Faker<Product>()
            .CustomInstantiator(s => new Product(
                s.Random.String(10, 50),
                s.Finance.Amount(1, 100),
                s.Random.String(1, 250),
                s.Random.String(1, 250),
                s.Random.String(1, 250),
                s.Random.Double(),
                s.Random.Int(100)
             ));


        public static Product GenerateValidProduct()
        {
            return ProductItemFaker.Generate();
        }

        public static List<Product> GenerateValidProduct(int itemCount = 1)
        {
            return ProductItemFaker.Generate(itemCount);
        }
    }
}
