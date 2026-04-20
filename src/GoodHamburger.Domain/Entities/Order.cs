using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

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
        ValidateDuplicateCategory(item.Category);

        _items.Add(item);
    }

    public void ClearItems()
    {
        _items.Clear();
    }

    private void ValidateDuplicateCategory(MenuCategory category)
    {
        if (_items.Any(x => x.Category == category))
            throw new DomainException($"Apenas um item permitido para esta categoria. {category}.");
    }

    private decimal CalculateDiscount()
    {
        bool hasSandwich = _items.Any(x => x.Category == MenuCategory.Sandwich);
        bool hasSide = _items.Any(x => x.Category == MenuCategory.Side);
        bool hasDrink = _items.Any(x => x.Category == MenuCategory.Drink);

        if (hasSandwich && hasSide && hasDrink)
            return Subtotal * 0.20m;

        if (hasSandwich && hasDrink)
            return Subtotal * 0.15m;

        if (hasSandwich && hasSide)
            return Subtotal * 0.10m;

        return 0m;
    }
}