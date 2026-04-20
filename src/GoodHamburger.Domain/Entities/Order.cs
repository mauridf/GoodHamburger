using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;

    public decimal Subtotal => _items.Sum(x => x.Total);

    public decimal Discount => CalculateDiscount();

    public decimal Total => Subtotal - Discount;

    public Order()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(OrderItem item)
    {
        _items.Add(item);
    }

    public void ClearItems()
    {
        _items.Clear();
    }

    private decimal CalculateDiscount()
    {
        int sandwiches = _items
            .Where(x => x.Category == MenuCategory.Sandwich)
            .Sum(x => x.Quantity);

        int sides = _items
            .Where(x => x.Category == MenuCategory.Side)
            .Sum(x => x.Quantity);

        int drinks = _items
            .Where(x => x.Category == MenuCategory.Drink)
            .Sum(x => x.Quantity);

        decimal sandwichPrice = GetPrice(MenuCategory.Sandwich);
        decimal sidePrice = GetPrice(MenuCategory.Side);
        decimal drinkPrice = GetPrice(MenuCategory.Drink);

        decimal discount = 0m;

        while (sandwiches > 0 && sides > 0 && drinks > 0)
        {
            discount += (sandwichPrice + sidePrice + drinkPrice) * 0.20m;
            sandwiches--;
            sides--;
            drinks--;
        }

        while (sandwiches > 0 && drinks > 0)
        {
            discount += (sandwichPrice + drinkPrice) * 0.15m;
            sandwiches--;
            drinks--;
        }

        while (sandwiches > 0 && sides > 0)
        {
            discount += (sandwichPrice + sidePrice) * 0.10m;
            sandwiches--;
            sides--;
        }

        return decimal.Round(discount, 2);
    }

    private decimal GetPrice(MenuCategory category)
    {
        return _items
            .FirstOrDefault(x => x.Category == category)?.UnitPrice ?? 0m;
    }
}