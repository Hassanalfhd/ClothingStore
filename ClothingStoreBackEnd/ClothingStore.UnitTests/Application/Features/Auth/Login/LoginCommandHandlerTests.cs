using ClothingStore.Application.Features.Auth.DTOs;
using ClothingStore.Application.Features.Auth.Login;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using FluentAssertions;
using Moq;

namespace ClothingStore.UnitTests.Application.Features.Auth.Login
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {

            _identityServiceMock = new Mock<IIdentityService>();

            _handler = new LoginCommandHandler(_identityServiceMock.Object);

        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            //1.Arrange
            var command = new LoginCommand("test@example.com", "Password123!");
            var expectedResponse = new AuthResponseDto("fake-access-token", "fake-refresh-token");


            _identityServiceMock.Setup(s => s.LoginAsync(command.Email, command.Password, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<AuthResponseDto>.Success(expectedResponse));

            //2.Act
            var result = await _handler.Handle(command, CancellationToken.None);
                 
            //3.Assert 
            result.IsSuccess.Should().BeTrue();
            result.Value.AccessToken.Should().Be("fake-access-token");
            result.Value.RefreshToken.Should().Be("fake-refresh-token");

    
        }



        [Fact]
        public async Task Handle_Should_Return_Failure_When_Identity_Service_Fails()
        {
            //1.Arrange
            var command = new LoginCommand("wrong@test.com", "wrong-passw");

            var expectedResult = Result<AuthResponseDto>.Failure("Invalid credentials");

            _identityServiceMock
                .Setup(s => s.LoginAsync(command.Email, command.Password, It.IsAny<CancellationToken>()))                
                .ReturnsAsync(expectedResult);


            //2.Act 
            var result = await _handler.Handle(command, CancellationToken.None);

            //3.Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Invalid credentials");

        }

    }
}
