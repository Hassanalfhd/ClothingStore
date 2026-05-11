using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Products.Commands.CreateProduct;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
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
        public async Task Handle_Should_Create_Product_Successfully()
        {
            // Arrange 
            var command = new CreateProductCommand("Nike T-Shirt", "none", 10, "YR", true, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            _brandRepoMock.Setup(x => x.GetIdAsync(command.BrandId, It.IsAny<CancellationToken>())).ReturnsAsync(10);
            _categoryRepoMock.Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>())).ReturnsAsync(10);
            _userRepoMock.Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>())).ReturnsAsync(10);
            
            _productRepoMock.Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(1);

            // Act 

            var result = await _handler.Handle(command, CancellationToken.None) ;

            // Assert 
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
            

            _productRepoMock.Verify(x =>
                x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),
                Times.Once);


            _unitOfWorkMock.Verify(x =>
                x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);

        }


        [Fact]
        public async Task Handle_Should_Return_Failure_When_Brand_NotFound()
        {
            var command = new CreateProductCommand("Nike T-Shirt", "none", 10, "YR", true, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            _brandRepoMock.Setup(x => x.GetIdAsync(command.BrandId, It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);
            _categoryRepoMock.Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>())).ReturnsAsync(10);
            _userRepoMock.Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>())).ReturnsAsync(10);


            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Brand");
        }


        [Fact]
        public async Task Handle_Should_Return_Failure_When_Category_NotFound()
        {
            var command = new CreateProductCommand("Nike T-Shirt", "none", 10, "YR", true, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            _categoryRepoMock.Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);
            _brandRepoMock.Setup(x => x.GetIdAsync(command.BrandId, It.IsAny<CancellationToken>())).ReturnsAsync(10);
            _userRepoMock.Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>())).ReturnsAsync(10);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Category");
        }



        [Fact]
        public async Task Handle_Should_Return_Failure_When_User_NotFound()
        {
            var command = new CreateProductCommand("Nike T-Shirt", "none", 10, "YR", true, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            _userRepoMock.Setup(x => x.GetIdAsync(command.CreatedBy, It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);
            _brandRepoMock.Setup(x => x.GetIdAsync(command.BrandId, It.IsAny<CancellationToken>())).ReturnsAsync(10);
            _categoryRepoMock.Setup(x => x.GetIdAsync(command.CategoryId, It.IsAny<CancellationToken>())).ReturnsAsync(10);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("User");
        }
    }
}
