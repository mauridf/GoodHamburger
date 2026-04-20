using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Services;

public static class DiscountCalculator
{
    public static decimal Calculate(IReadOnlyCollection<OrderItem> items)
    {
        int sandwiches = GetQuantity(items, MenuCategory.Sandwich);
        int sides = GetQuantity(items, MenuCategory.Side);
        int drinks = GetQuantity(items, MenuCategory.Drink);

        decimal sandwichPrice = GetAveragePrice(items, MenuCategory.Sandwich);
        decimal sidePrice = GetAveragePrice(items, MenuCategory.Side);
        decimal drinkPrice = GetAveragePrice(items, MenuCategory.Drink);

        decimal discount = 0m;

        while (sandwiches > 0 && sides > 0 && drinks > 0)
        {
            var comboTotal = sandwichPrice + sidePrice + drinkPrice;
            discount += comboTotal * 0.20m;

            sandwiches--;
            sides--;
            drinks--;
        }

        while (sandwiches > 0 && drinks > 0)
        {
            var comboTotal = sandwichPrice + drinkPrice;
            discount += comboTotal * 0.15m;

            sandwiches--;
            drinks--;
        }

        while (sandwiches > 0 && sides > 0)
        {
            var comboTotal = sandwichPrice + sidePrice;
            discount += comboTotal * 0.10m;

            sandwiches--;
            sides--;
        }

        return decimal.Round(discount, 2);
    }

    private static int GetQuantity(
        IReadOnlyCollection<OrderItem> items,
        MenuCategory category)
    {
        return items
            .Where(x => x.Category == category)
            .Sum(x => x.Quantity);
    }

    private static decimal GetAveragePrice(
        IReadOnlyCollection<OrderItem> items,
        MenuCategory category)
    {
        var selected = items
            .Where(x => x.Category == category)
            .ToList();

        if (!selected.Any())
            return 0m;

        return selected.First().UnitPrice;
    }
}