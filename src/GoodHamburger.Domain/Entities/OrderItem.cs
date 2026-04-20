using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid MenuItemId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public MenuCategory Category { get; private set; }

    public OrderItem(Guid menuItemId, string name, decimal unitPrice, MenuCategory category)
    {
        Id = Guid.NewGuid();
        MenuItemId = menuItemId;
        Name = name;
        UnitPrice = unitPrice;
        Category = category;
    }
}