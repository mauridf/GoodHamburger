using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }

    public Guid MenuItemId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public MenuCategory Category { get; private set; }

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public decimal Total => UnitPrice * Quantity;

    public OrderItem(
        Guid menuItemId,
        string name,
        decimal unitPrice,
        MenuCategory category,
        int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantidade tem que ser maior que zero.");

        Id = Guid.NewGuid();
        MenuItemId = menuItemId;
        Name = name;
        UnitPrice = unitPrice;
        Category = category;
        Quantity = quantity;
    }
}