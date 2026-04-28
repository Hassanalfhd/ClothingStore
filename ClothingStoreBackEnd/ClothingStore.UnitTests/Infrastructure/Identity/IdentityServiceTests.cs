using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Entities;
using ClothingStore.Infrastructure.Identity.Services;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Identity.Models;
using ClothingStore.Infrastructure.Settings;
using ClothingStore.Infrastructure.Persistence;

namespace ClothingStore.UnitTests.Infrastructure.Identity;

public class IdentityServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<IAppLogger<IdentityService>> _loggerMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepo> _userRepoMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IRefreshTokenRepo> _refreshTokenRepoMock;
    private readonly IOptions<JwtSettings> _jwtSettings;

    private readonly IdentityService _service;

    public IdentityServiceTests()
    {
        _userManagerMock = CreateUserManagerMock();
        _loggerMock = new Mock<IAppLogger<IdentityService>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepoMock = new Mock<IUserRepo>();
        _tokenServiceMock = new Mock<ITokenService>();
        _refreshTokenRepoMock = new Mock<IRefreshTokenRepo>();
        

        _jwtSettings = Options.Create(new JwtSettings
        {
            AccessTokenMinutes = 30,
            RefreshTokenDays = 7
            
        });

        _unitOfWorkMock
        .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(1);


        _service = new IdentityService(
            _userManagerMock.Object,
            //Mock.Of<ApplicationDbContext>(), // DbContext not used in unit tests here
           null!,
            _loggerMock.Object,
            _unitOfWorkMock.Object,
            _userRepoMock.Object,
            _tokenServiceMock.Object,
            _jwtSettings,
            _refreshTokenRepoMock.Object
        );
    }

    #region REGISTER TESTS

    [Fact]
    public async Task RegisterAsync_Should_Return_Success_When_User_Created()
    {
        // Arrange
        var email = "test@test.com";
        var password = "Password123!";
        var firstName = "Hassan";
        var lastName = "Alfahd";

        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), password))
            .ReturnsAsync(IdentityResult.Success);

        _userManagerMock
            .Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Customer"))
            .ReturnsAsync(IdentityResult.Success);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _service.RegisterAsync(email, password, firstName, lastName);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task RegisterAsync_Should_Return_Failure_When_CreateUser_Fails()
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError
            {
                Description = "User creation failed"
            }));

        // Act
        var result = await _service.RegisterAsync("test@test.com", "123456", "A", "B");

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    #endregion

    #region LOGIN TESTS

    [Fact]
    public async Task LoginAsync_Should_Return_Success_When_Credentials_Are_Valid()
    {
        // Arrange
        var user = new ApplicationUser("test@test.com", "Hassan", "Alfahd");

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>()))
            .ReturnsAsync(true);

        _userManagerMock
            .Setup(x => x.GetRolesAsync(user))
            .ReturnsAsync(new List<string> { "Customer" });

        _tokenServiceMock
            .Setup(x => x.CreateAccessTokenAsync(It.IsAny<TokenRequestDto>()))
            .ReturnsAsync("access-token");

        _tokenServiceMock
            .Setup(x => x.CreateRefreshToken())
            .Returns("refresh-token");

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _service.LoginAsync("test@test.com", "Password123!");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.AccessToken.Should().Be("access-token");
        result.Value.RefreshToken.Should().Be("refresh-token");
    }

    [Fact]
    public async Task LoginAsync_Should_Return_Failure_When_User_Not_Found()
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _service.LoginAsync("test@test.com", "123");

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    #endregion

    #region REFRESH TOKEN TESTS

    [Fact]
    public async Task RefreshTokenAsync_Should_Return_Failure_When_Token_Not_Found()
    {
        // Arrange
        _refreshTokenRepoMock
            .Setup(x => x.GetRefreshTokenByTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((RefreshToken?)null);

        // Act
        var result = await _service.RefreshTokenAsync("token", "access");

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task RefreshTokenAsync_Should_Return_Success_When_Token_Is_Valid()
    {
        // Arrange
        var storedToken = new RefreshToken(1, "old-refresh-token", 7);

        _refreshTokenRepoMock
            .Setup(x => x.GetRefreshTokenByTokenAsync("old-refresh-token", It.IsAny<CancellationToken>()))
            .ReturnsAsync(storedToken);

        var tokenRequest = new TokenRequestDto
        {
            Email = "test@test.com",
            publicId = Guid.NewGuid().ToString(),
            Roles = new List<string> { "Customer" }
        };

        _tokenServiceMock
            .Setup(x => x.GetTokenRequestDtoFromToken("access-token"))
            .Returns(tokenRequest);

        _tokenServiceMock
            .Setup(x => x.CreateAccessTokenAsync(tokenRequest))
            .ReturnsAsync("new-access-token");

        _tokenServiceMock
            .Setup(x => x.CreateRefreshToken())
            .Returns("new-refresh-token");

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _service.RefreshTokenAsync("old-refresh-token", "access-token");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.AccessToken.Should().Be("new-access-token");
        result.Value.RefreshToken.Should().Be("new-refresh-token");
    }

    #endregion

    #region LOGOUT TESTS

    [Fact]
    public async Task LogoutAsync_Should_Return_Failure_When_Token_Not_Found()
    {
        // Arrange
        _refreshTokenRepoMock
            .Setup(x => x.GetRefreshTokenByTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((RefreshToken?)null);

        // Act
        var result = await _service.LogoutAsync("token");

        // Assert
        result.IsSuccess.Should().BeFalse();
    }


    [Fact]
    public async Task LogoutAsync_Should_Return_Success_When_Token_Is_Valid()
    {
        // Arrange
        var storedToken = new RefreshToken(1, "refresh-token", 7);

        _refreshTokenRepoMock
            .Setup(x => x.GetRefreshTokenByTokenAsync("refresh-token", It.IsAny<CancellationToken>()))
            .ReturnsAsync(storedToken);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _service.LogoutAsync("refresh-token");

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    #endregion

    #region USERMANAGER MOCK FACTORY

    private static Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();

        return new Mock<UserManager<ApplicationUser>>(
            store.Object,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!
        );
    }

    #endregion
}