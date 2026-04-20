using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public MenuCategory Category { get; private set; }

    public MenuItem(Guid id, string name, decimal price, MenuCategory category)
    {
        Id = id;
        Name = name;
        Price = price;
        Category = category;
    }
}