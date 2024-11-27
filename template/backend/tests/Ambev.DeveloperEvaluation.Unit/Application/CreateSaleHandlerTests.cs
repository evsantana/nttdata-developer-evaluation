using Ambev.DeveloperEvaluation.Application.Sales.Handlers;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _repository;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _repository = Substitute.For<ISaleRepository>();
            _handler = new CreateSaleHandler(_repository);
        }

        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale(
               command.SaleNumber,
               command.UserId,
               command.TotalAmount,
               command.Branch,
               command.SaleItems
            );

            _repository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            createSaleResult.Should().NotBeNull();
            createSaleResult.Id.Should().Be(sale.Id);

            await _repository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());

        }
    }
}
