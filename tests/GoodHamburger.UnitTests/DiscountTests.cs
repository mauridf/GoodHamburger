using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.UnitTests;

public class DiscountTests
{
    [Fact]
    public void Should_Apply_Multiple_Combos_Correctly()
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
            2.5m,
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
}