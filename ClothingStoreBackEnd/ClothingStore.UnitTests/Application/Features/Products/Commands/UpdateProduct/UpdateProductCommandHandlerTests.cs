using ClothingStore.Application.DTOs;
using ClothingStore.Application.Features.Products.Commands.UpdateProduct;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace ClothingStore.UnitTests.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandlerTests
    {

        private readonly Mock<IProductRepo> _productRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepo> _userRepoMock;
        private readonly Mock<ICategoryRepo> _categoryRepoMock;
        private readonly Mock<IBrandRepo> _brandRepoMock;

        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _productRepoMock = new Mock<IProductRepo>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _categoryRepoMock = new Mock<ICategoryRepo>();
            _userRepoMock = new Mock<IUserRepo>();
            _brandRepoMock = new Mock<IBrandRepo>();

            _handler = new UpdateProductCommandHandler(_productRepoMock.Object,
                _unitOfWorkMock.Object, _categoryRepoMock.Object,
                _userRepoMock.Object, _brandRepoMock.Object
                );

        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_Product_NotFound()
        {
            var command = UpdateProductCommand();

            _productRepoMock.Setup(x => x.GetByIdAsync(command.PublicId, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);

            var result = await _handler.Handle(command);


            result.IsSuccess.Should().BeFalse();

        }



        [Fact]
        public async Task Handle_Should_Return_Failure_When_Category_NotFound()
        {
            var command = UpdateProductCommand();

            _categoryRepoMock.Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }



        [Fact]
        public async Task Handle_Should_Return_Failure_When_User_NotFound()
        {
            var command = UpdateProductCommand();
            _userRepoMock.Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_Brand_NotFound()
        {
            var command = UpdateProductCommand();

            _brandRepoMock.Setup(x => x.GetIdAsync(command.BrandId, It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_Should_Update_Product_Successfully()
        {
            // Arrange
            var product = new Product(
                "Old Name",
                "Old Desc",
                new Money(100, "SR"),
                true,
                1, 1, 1);

            var command = new UpdateProductCommand
            (
                product.PublicId ,
                "New Name",
                "New Desc",
                200,
                "USD",
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),

                new List<ProductSpecificationDto>
        {
            new()
            {
                Key = "Color",
                Value = "Black"
            }
        }
            );

            _productRepoMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            _categoryRepoMock
                .Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(10);

            _userRepoMock
                .Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(20);

            _brandRepoMock
                .Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(30);

            _unitOfWorkMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            product.Name.Should().Be(command.Name);
            product.Description.Should().Be(command.Description);

            product.BasePrice.Amount.Should().Be(command.Price);
            product.BasePrice.Currency.Should().Be(command.Currency);

            product.CategoryId.Should().Be(10);
            product.BrandId.Should().Be(30);
            product.CreatedBy.Should().Be(20);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }


        private static UpdateProductCommand UpdateProductCommand()
        {
            return new UpdateProductCommand(Guid.NewGuid(), "Nike T-Shirt", "none", 10, "YR", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

        }
    }
}
