using ClothingStore.Application.DTOs;
using ClothingStore.Application.Features.Products.Commands.UpdateProduct;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using ClothingStore.Infrastructure.Persistence.Repositories;
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
            var command = CreateValidUpdateCommand();

            _productRepoMock.Setup(x => x.GetByIdAsync(command.PublicId, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);

            var result = await _handler.Handle(command);


            result.IsSuccess.Should().BeFalse();

        }



        [Fact]
        public async Task Handle_Should_Return_Failure_When_Category_NotFound()
        {
            var command = CreateValidUpdateCommand();

            _categoryRepoMock.Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }


        [Fact]
        public async Task Handle_Should_Replace_Product_Specifications()
        {
            // Arrange
            var product = new Product(
                "Old",
                "Desc",
                new Money(100, "USD"),
                true,
                1,
                1,
                1);

            product.AddSepecifiaction("OldKey", "OldValue");

            _productRepoMock
                .Setup(x => x.GetByIdAsync(product.PublicId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            _categoryRepoMock.Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _userRepoMock.Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _brandRepoMock.Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new UpdateProductCommand
            (
                 product.PublicId,
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
                        Key="Brand",
                        Value = "Nike",
                  }
                }
            );



            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert

            product.Specifications.Should().NotContain(
                s => s.Key == "OldKey");

            product.Specifications.Should().Contain(
                s => s.Key == "Brand" && s.Value == "Nike");
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_User_NotFound()
        {
            var command = CreateValidUpdateCommand();
            _userRepoMock.Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_Brand_NotFound()
        {
            var command = CreateValidUpdateCommand();

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
                new Money(100, "USD"),
                true,
                1,
                1,
                1
                );

            product.AddSepecifiaction("Brand", "OldBrand");

            _productRepoMock
                .Setup(x => x.GetByIdAsync(product.PublicId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            _categoryRepoMock
                .Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _userRepoMock
                .Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            _brandRepoMock
                .Setup(x => x.GetIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(3);

            var command = new UpdateProductCommand
            (
                 product.PublicId,
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
                        Key="Brand",
                        Value = "Nike",
                  }
                }
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            product.Name.Should().Be("New Name");

            product.Description.Should().Be("New Desc");
            
            product.Specifications.Should().Contain(
            s => s.Key == "Brand" && s.Value == "Nike");

            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private static UpdateProductCommand CreateValidUpdateCommand()
        {
            return  new UpdateProductCommand
            (
                 Guid.NewGuid(),
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
                        Key="Brand",
                        Value = "Nike",
                  }
                }
            );

        }
    }
}
