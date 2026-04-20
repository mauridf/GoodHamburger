using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.UnitTests;

public class OrderTests
{
    [Fact]
    public void Should_Not_Apply_Discount_When_Only_Drink()
    {
        var order = new Order();

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Drink",
            3m,
            MenuCategory.Drink,
            2));

        Assert.Equal(6m, order.Subtotal);
        Assert.Equal(0m, order.Discount);
        Assert.Equal(6m, order.Total);
    }

    [Fact]
    public void Should_Apply_20_Percent_When_Full_Combo()
    {
        var order = new Order();

        order.AddItem(new OrderItem(Guid.NewGuid(), "Burger", 5m, MenuCategory.Sandwich, 1));
        order.AddItem(new OrderItem(Guid.NewGuid(), "Fries", 2m, MenuCategory.Side, 1));
        order.AddItem(new OrderItem(Guid.NewGuid(), "Drink", 3m, MenuCategory.Drink, 1));

        Assert.Equal(10m, order.Subtotal);
        Assert.Equal(2m, order.Discount);
        Assert.Equal(8m, order.Total);
    }

    [Fact]
    public void Should_Apply_15_Percent_When_Burger_And_Drink()
    {
        var order = new Order();

        order.AddItem(new OrderItem(Guid.NewGuid(), "Burger", 6m, MenuCategory.Sandwich, 1));
        order.AddItem(new OrderItem(Guid.NewGuid(), "Drink", 4m, MenuCategory.Drink, 1));

        Assert.Equal(10m, order.Subtotal);
        Assert.Equal(1.5m, order.Discount);
        Assert.Equal(8.5m, order.Total);
    }

    [Fact]
    public void Should_Apply_10_Percent_When_Burger_And_Fries()
    {
        var order = new Order();

        order.AddItem(new OrderItem(Guid.NewGuid(), "Burger", 8m, MenuCategory.Sandwich, 1));
        order.AddItem(new OrderItem(Guid.NewGuid(), "Fries", 2m, MenuCategory.Side, 1));

        Assert.Equal(10m, order.Subtotal);
        Assert.Equal(1m, order.Discount);
        Assert.Equal(9m, order.Total);
    }

    [Fact]
    public void Should_Clear_Items_On_Update()
    {
        var order = new Order();

        order.AddItem(new OrderItem(Guid.NewGuid(), "Burger", 5m, MenuCategory.Sandwich, 2));
        order.AddItem(new OrderItem(Guid.NewGuid(), "Drink", 2m, MenuCategory.Drink, 1));

        Assert.Equal(2, order.Items.Count);

        order.ClearItems();

        Assert.Empty(order.Items);
        Assert.Equal(0m, order.Subtotal);
    }

    [Fact]
    public void Should_Calculate_Total_By_Quantity()
    {
        var order = new Order();

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Burger",
            5m,
            MenuCategory.Sandwich,
            3));

        Assert.Equal(15m, order.Subtotal);
    }

    [Fact]
    public void Should_Apply_Multiple_Combo_Priority()
    {
        var order = new Order();

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Burger",
            5m,
            MenuCategory.Sandwich,
            2));

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Drink",
            2.50m,
            MenuCategory.Drink,
            2));

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Fries",
            2m,
            MenuCategory.Side,
            1));

        Assert.Equal(17m, order.Subtotal);
        Assert.Equal(3.03m, order.Discount);
        Assert.Equal(13.97m, order.Total);
    }

    [Fact]
    public void Should_Not_Accept_Quantity_Zero()
    {
        Assert.Throws<DomainException>(() =>
            new OrderItem(
                Guid.NewGuid(),
                "Burger",
                5m,
                MenuCategory.Sandwich,
                0));
    }

    [Fact]
    public void Should_Not_Accept_Quantity_Negative()
    {
        Assert.Throws<DomainException>(() =>
            new OrderItem(
                Guid.NewGuid(),
                "Burger",
                5m,
                MenuCategory.Sandwich,
                -1));
    }

    [Fact]
    public void Should_Round_Discount_To_2_Decimals()
    {
        var order = new Order();

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Burger",
            5m,
            MenuCategory.Sandwich,
            2));

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Drink",
            2.50m,
            MenuCategory.Drink,
            2));

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Fries",
            2m,
            MenuCategory.Side,
            1));

        Assert.Equal(3.03m, order.Discount);
    }

    [Fact]
    public void Should_Return_Total_After_Multiple_Discounts()
    {
        var order = new Order();

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Burger",
            5m,
            MenuCategory.Sandwich,
            3));

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Drink",
            2.50m,
            MenuCategory.Drink,
            2));

        order.AddItem(new OrderItem(
            Guid.NewGuid(),
            "Fries",
            2m,
            MenuCategory.Side,
            1));

        Assert.Equal(22m, order.Subtotal);
        Assert.Equal(4.15m, order.Discount);
        Assert.Equal(17.85m, order.Total);
    }
}