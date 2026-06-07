using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using FluentAssertions;

namespace ClothingStore.UnitTests.Domain;

public class CartItemTests
{
    [Fact]
    public void Constructor_Should_Create_Item_With_Valid_Data()
    {
        var item = new CartItem(
            1,
            10,
            20,
            Guid.NewGuid(),
            "Nike",
            new Money(100, "USD"),
            2);

        item.Quantity.Should().Be(2);

        item.ProductName.Should().Be("Nike");
    }

    [Fact]
    public void UpdateQuantity_Should_Update_Quantity()
    {
        var item = new CartItem(
            1,
            10,
            20,
            Guid.NewGuid(),
            "Nike",
            new Money(100, "USD"),
            2);

        item.UpdateQuantity(5);

        item.Quantity.Should().Be(5);
    }

    [Fact]
    public void UpdateQuantity_Should_Throw_When_Quantity_Is_Zero()
    {
        var item = new CartItem(
            1,
            10,
            20,
            Guid.NewGuid(),
            "Nike",
            new Money(100, "USD"),
            2);

        Action act = () => item.UpdateQuantity(0);

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IncreaseQuantity_Should_Increase_By_One()
    {
        var item = new CartItem(
            1,
            10,
            20,
            Guid.NewGuid(),
            "Nike",
            new Money(100, "USD"),
            2);

        item.IncreaseQuantity();

        item.Quantity.Should().Be(3);
    }

    [Fact]
    public void IncreaseQuantity_With_Parameter_Should_Increase_By_Specified_Amount()
    {
        var item = new CartItem(
            1,
            10,
            20,
            Guid.NewGuid(),
            "Nike",
            new Money(100, "USD"),
            2);

        item.IncreaseQuantity(5);

        item.Quantity.Should().Be(7);
    }

    [Fact]
    public void DecreaseQuantity_Should_Decrease_By_One()
    {
        var item = new CartItem(
            1,
            10,
            20,
            Guid.NewGuid(),
            "Nike",
            new Money(100, "USD"),
            3);

        item.DecreaseQuantity();

        item.Quantity.Should().Be(2);
    }

    [Fact]
    public void DecreaseQuantity_Should_Throw_When_Quantity_Is_One()
    {
        var item = new CartItem(
            1,
            10,
            20,
            Guid.NewGuid(),
            "Nike",
            new Money(100, "USD"),
            1);

        Action act = () => item.DecreaseQuantity();

        act.Should().Throw<InvalidOperationException>();
    }
}