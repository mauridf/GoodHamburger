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

        order.AddItem(new OrderItem(Guid.NewGuid(), "Drink", 3m, MenuCategory.Drink, 1));

        Assert.Equal(3m, order.Subtotal);
        Assert.Equal(0m, order.Discount);
        Assert.Equal(3m, order.Total);
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
    public void Should_Clear_Items()
    {
        var order = new Order();

        order.AddItem(new OrderItem(Guid.NewGuid(), "Burger", 5m, MenuCategory.Sandwich, 1));

        order.ClearItems();

        Assert.Empty(order.Items);
        Assert.Equal(0m, order.Subtotal);
    }

    [Fact]
    public void Should_Not_Accept_Quantity_Zero()
    {
        Assert.Throws<DomainException>(() =>
            new OrderItem(Guid.NewGuid(), "Burger", 5m, MenuCategory.Sandwich, 0));
    }

    [Fact]
    public void Should_Not_Accept_Quantity_Negative()
    {
        Assert.Throws<DomainException>(() =>
            new OrderItem(Guid.NewGuid(), "Burger", 5m, MenuCategory.Sandwich, -1));
    }
}