using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.Features.Catalog.Cart;
using ClothingStore.Application.Features.Catalog.Cart.Dtos;
using ClothingStore.Application.Features.Products.Queries.GetProductById;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ClothingStore.UnitTests.Application.Features.Catlog
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepo> _cartRepoMock = new();
        private readonly Mock<IUserRepo> _userRepoMock = new();
        private readonly Mock<IProductReadRepos> _productRepoMock = new();
        private readonly Mock<IProductVariantRepo> _variantRepoMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        private readonly CartService _service;

        public CartServiceTests()
        {
            _service = new CartService(
                _cartRepoMock.Object,
                _userRepoMock.Object,
                _productRepoMock.Object,
                _variantRepoMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddToCart_Should_Return_Failure_When_Variant_Not_Found()
        {
            var dto = new AddToCartDto
            (
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                2
            );

            _variantRepoMock
                .Setup(x => x.GetVariantDtoByIdAsync(
                    dto.VariantId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((ProductVariantCartDto?)null);

            var result = await _service.AddToCart(
                dto,
                CancellationToken.None);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task AddToCart_Should_Return_Failure_When_Product_Not_Found()
        {
            var dto = new AddToCartDto
          (
              Guid.NewGuid(),
              Guid.NewGuid(),
              Guid.NewGuid(),
              2
          );

            _variantRepoMock
                .Setup(x => x.GetVariantDtoByIdAsync(
                    dto.VariantId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductVariantCartDto
                {
                    Id = 1,
                    StockQuantity = 100,
                    PublicId = Guid.NewGuid(),
                    Price = 100,
                    Currency = "USD"
                });

            _productRepoMock
                .Setup(x => x.GetByIdAsync(
                    dto.ProductId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((ProductDto?)null);

            var result = await _service.AddToCart(
                dto,
                CancellationToken.None);

            result.IsFailure.Should().BeTrue();
        }
        [Fact]
        public async Task AddToCart_Should_Return_Failure_When_Stock_Is_Insufficient()
        {
            var dto = new AddToCartDto
            (
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                20
            );

            _variantRepoMock
                .Setup(x => x.GetVariantDtoByIdAsync(
                    dto.VariantId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductVariantCartDto
                {
                    Id = 1,
                    StockQuantity = 5
                });

            var result = await _service.AddToCart(
                dto,
                CancellationToken.None);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task AddToCart_Should_Create_Cart_When_Cart_Does_Not_Exist()
        {
            var userGuid = Guid.NewGuid();

            var dto = new AddToCartDto
            (
                userGuid,
                Guid.NewGuid(),
                Guid.NewGuid(),
                20
            );

            _userRepoMock
                .Setup(x => x.GetIdAsync(
                    userGuid,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _variantRepoMock
                .Setup(x => x.GetVariantDtoByIdAsync(
                    dto.VariantId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductVariantCartDto
                {
                    Id = 1,
                    PublicId = Guid.NewGuid(),
                    StockQuantity = 100,
                    Price = 100,
                    Currency = "USD"
                });

            _productRepoMock
                .Setup(x => x.GetByIdAsync(
                    dto.ProductId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductDto
                {
                    Id = 1,
                    ProductName = "Nike"
                });

            _cartRepoMock
                .Setup(x => x.GetByUserIdAsync(
                    1,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Cart?)null);

            var result = await _service.AddToCart(
                dto,
                CancellationToken.None);

            result.IsSuccess.Should().BeTrue();

            _cartRepoMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Cart>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }


        [Fact]
        public async Task RemoveFromCart_Should_Return_Failure_When_Cart_Not_Found()
        {
            var dto = new ManageCartItemQuantityDto
            {
                UserId = Guid.NewGuid(),
                CartItemPublicId = Guid.NewGuid()
            };

            _userRepoMock
                .Setup(x => x.GetIdAsync(
                    dto.UserId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _cartRepoMock
                .Setup(x => x.GetByUserIdAsync(
                    1,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Cart?)null);

            var result = await _service.RemoveFromCart(
                dto,
                CancellationToken.None);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateQuantity_Should_Return_Failure_When_Quantity_Is_Zero()
        {
            var dto = new UpdateQuantityDto
            {
                UserId = Guid.NewGuid(),
                CartItemPublicId = Guid.NewGuid(),
                Quantity = 0
            };

            _userRepoMock
                .Setup(x => x.GetIdAsync(
                    dto.UserId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _cartRepoMock
                .Setup(x => x.GetByUserIdAsync(
                    1,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Cart(1));

            var result = await _service.UpdateQuantityAsync(
                dto,
                CancellationToken.None);

            result.IsFailure.Should().BeTrue();
        }

    }
}
