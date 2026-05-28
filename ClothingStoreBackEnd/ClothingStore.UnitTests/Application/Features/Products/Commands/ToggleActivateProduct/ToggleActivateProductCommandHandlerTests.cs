using ClothingStore.Application.Features.Products.Commands.ToggleActivateProduct;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace ClothingStore.UnitTests.Application.Features.Products.Commands.ToggleActivateProduct
{
    public  class ToggleActivateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepo> _productRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly ToggleActivateCommandHandler _handler;
        public ToggleActivateProductCommandHandlerTests()
        {
            _productRepoMock = new Mock<IProductRepo>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new ToggleActivateCommandHandler(_productRepoMock.Object, _unitOfWorkMock.Object);

        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_Product_NotFound()
        {
            var command = new ToggleActivateCommand(Guid.NewGuid());

            _productRepoMock.Setup(x=>x.GetByIdAsync(command.PublicId, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);

            var result = await _handler.Handle(command, CancellationToken.None);
        
            
            result.IsSuccess.Should().BeFalse();
        }


        [Fact]
        public async Task Handle_Should_Toggle_Activate_Product_Successfully()
        {
            var product = new Product(
                      "Old Name",
                      "Old Desc",
                      new Money(100, "SR"),
                      true,
                      1, 1, 1);

            var command = new ToggleActivateCommand(product.PublicId);

            _productRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);

            var result = await _handler.Handle(command, CancellationToken.None);
            
            result.IsSuccess.Should().BeTrue();

            _unitOfWorkMock.Verify(
               x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
               Times.Once);

        }




    }
}
