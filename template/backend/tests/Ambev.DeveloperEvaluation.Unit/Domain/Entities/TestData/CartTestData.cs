using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class CartTestData
    {
        private static readonly Faker<CartItem> SaleItemFaker = new Faker<CartItem>()
            .CustomInstantiator(s => new CartItem(
            s.Random.Guid(),
            s.Random.Number(1, 100),
            s.Random.Guid()
        ));

        private static readonly Faker<Cart> SaleFaker = new Faker<Cart>()
            .CustomInstantiator(s => new Cart(
            s.Random.Guid(),
            SaleItemFaker.Generate(2)
        ));

        public static Cart GenerateValidCart()
        {
            return SaleFaker.Generate();
        }

        public static CartItem GenerateValidCartItem()
        {
            return SaleItemFaker.Generate();
        }
    }
}
