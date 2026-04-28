using ClothingStore.Application.Features.Auth.Register;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using Moq;
using FluentAssertions;

namespace ClothingStore.UnitTests.Application.Features.Auth.Register;

public class RegisterCommandHandlerTests
{
    private readonly Mock<IIdentityService> _identityServiceMock;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _handler = new RegisterCommandHandler(_identityServiceMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_Register_Is_Valid()
    {
        // Arrange
        var command = new RegisterCommand
        (
          "test@example.com",
          "Password123!",
          "Hassan",
          "Alfahd"
        );

        var expectedResult = Result<Guid>.Success(Guid.NewGuid());

        _identityServiceMock
            .Setup(x => x.RegisterAsync(
                command.Email,
                command.Password,
                command.FirstName,
                command.LastName,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _identityServiceMock.Verify(x =>
            x.RegisterAsync(
                command.Email,
                command.Password,
                command.FirstName,
                command.LastName,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Service_Fails()
    {
        // Arrange
        var command = new RegisterCommand
        (
            "test@example.com",
            "Password123!",
            "Hassan",
            "Alfahd"
        );

        var expectedResult = Result<Guid>.Failure("Error occurred");

        _identityServiceMock
            .Setup(x => x.RegisterAsync(
                command.Email,
                command.Password,
                command.FirstName,
                command.LastName,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}