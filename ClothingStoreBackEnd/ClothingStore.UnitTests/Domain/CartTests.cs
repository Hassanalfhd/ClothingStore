using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using FluentAssertions;

namespace ClothingStore.UnitTests.Domain;

public class CartTests
{
    [Fact]
    public void AddItem_Should_Add_New_Item_When_Variant_Does_Not_Exist()
    {
        var cart = new Cart(1);

        cart.AddItem(
            10,
            20,
            Guid.NewGuid(),
            "Nike Air Zoom",
            new Money(100, "USD"),
            2);

        cart.Items.Should().HaveCount(1);

        cart.Items.First().Quantity.Should().Be(2);
    }

    [Fact]
    public void AddItem_Should_Increase_Quantity_When_Variant_Already_Exists()
    {
        var cart = new Cart(1);

        var variantPublicId = Guid.NewGuid();

        cart.AddItem(
            10,
            20,
            variantPublicId,
            "Nike Air Zoom",
            new Money(100, "USD"),
            2);

        cart.AddItem(
            10,
            20,
            variantPublicId,
            "Nike Air Zoom",
            new Money(100, "USD"),
            3);

        cart.Items.Should().HaveCount(1);

        cart.Items.First().Quantity.Should().Be(5);
    }

    [Fact]
    public void AddItem_Should_Throw_When_Quantity_Is_Less_Than_One()
    {
        var cart = new Cart(1);

        Action act = () => cart.AddItem(
            10,
            20,
            Guid.NewGuid(),
            "Nike Air Zoom",
            new Money(100, "USD"),
            0);

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Clear_Should_Remove_All_Items()
    {
        var cart = new Cart(1);

        cart.AddItem(
            10,
            20,
            Guid.NewGuid(),
            "Nike",
            new Money(100, "USD"),
            2);

        cart.Clear();

        cart.Items.Should().BeEmpty();
    }

    [Fact]
    public void MarkAsCheckedOut_Should_Set_IsCheckedOut_To_True()
    {
        var cart = new Cart(1);

        cart.MarkAsCheckedOut();

        cart.IsCheckedOut.Should().BeTrue();
    }
}