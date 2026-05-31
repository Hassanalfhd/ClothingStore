using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.Features.Products.Commands.CreateProduct;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Infrastructure.Persistence.Repositories;
using FluentAssertions;
using Moq;

namespace ClothingStore.UnitTests.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepo> _productRepoMock;
        private readonly Mock<IBrandRepo> _brandRepoMock;
        private readonly Mock<ICategoryRepo> _categoryRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepo> _userRepoMock;


        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _productRepoMock = new Mock<IProductRepo>();
            _brandRepoMock = new Mock<IBrandRepo> ();
            _userRepoMock = new Mock<IUserRepo> ();
            _categoryRepoMock = new Mock<ICategoryRepo> ();
            _unitOfWorkMock = new Mock<IUnitOfWork>();


            _handler = new CreateProductCommandHandler(_productRepoMock.Object, _unitOfWorkMock.Object, _categoryRepoMock.Object, _userRepoMock.Object, _brandRepoMock.Object);

        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_Category_NotFound()
        {
            // Arrange
            var command = CreateValidCommand();

            _categoryRepoMock
                .Setup(x => x.GetIdAsync(command.CategoryId,It.IsAny<CancellationToken>()))
                .ReturnsAsync((long?)null);

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();

            result.Error.Should().Be("Category not found.");

            _productRepoMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(CancellationToken.None),
                Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_User_NotFound()
        {
            // Arrange
            var command = CreateValidCommand();

            _categoryRepoMock
                .Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _userRepoMock
                .Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>()))
                .ReturnsAsync((long?)null);

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();

            result.Error.Should().Be("User not found.");

            _productRepoMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(CancellationToken.None),
                Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_Brand_NotFound()
        {
            // Arrange
            var command = CreateValidCommand();

            _categoryRepoMock
                .Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _userRepoMock
                .Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            _brandRepoMock
                .Setup(x => x.GetIdAsync(command.BrandId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((long?)null);

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();

            result.Error.Should().Be("Brand not found.");

            _productRepoMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(CancellationToken.None),
                Times.Never);
        }

        [Fact]

        public async Task Handle_Should_Create_Product_Successfully()
        {
            // Arrange

            var categoryId = 1L;
            var brandId = 2L;
            var userId = 3L;

            var command = CreateValidCommand();

            _categoryRepoMock
                .Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(categoryId);

            _brandRepoMock
                .Setup(x => x.GetIdAsync(command.BrandId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(brandId);

            _userRepoMock
                .Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId);

            Product? createdProduct = null;

            _productRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Callback<Product, CancellationToken>((product, _) =>
                {
                    createdProduct = product;
                })
                .Returns(Task.CompletedTask);

            // Act

            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert

            result.IsSuccess.Should().BeTrue();

            createdProduct.Should().NotBeNull();

            createdProduct!.Name.Should().Be(command.Name);

            _productRepoMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(CancellationToken.None),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Add_Specifications_To_Product()
        {
            // Arrange
            var command = CreateValidCommand();

            _categoryRepoMock
                .Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _userRepoMock
                .Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            _brandRepoMock
                .Setup(x => x.GetIdAsync(command.BrandId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(3);

            Product? capturedProduct = null;

            _productRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Callback<Product, CancellationToken>((product, _) =>
                {
                    capturedProduct = product;
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            capturedProduct.Should().NotBeNull();

            capturedProduct!.Specifications.Should().NotBeEmpty();

            capturedProduct.Specifications.Should().Contain(
                s => s.Key == "Brand" && s.Value == "Nike");
        }

        private static CreateProductCommand CreateValidCommand()
        {
            return new CreateProductCommand
            (
                "Nike Air Zoom",
                "Running shoes",
                120,
                "USD",
                true,
                Guid.NewGuid(),
                Guid.NewGuid(),
                 Guid.NewGuid(),
                new List<ProductSpecificationDto>
                {
                  new()
                    {
                        Key = "Brand",
                        Value = "Nike"
                    }

                }
            );
        }
    }
}
