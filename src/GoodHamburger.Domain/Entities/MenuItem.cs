using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public string ImageUrl { get; private set; } = string.Empty;

    public MenuCategory Category { get; private set; }

    public MenuItem(
        Guid id,
        string name,
        string description,
        decimal price,
        string imageUrl,
        MenuCategory category)
    {
        Id = id;
        Update(name, description, price, imageUrl, category);
    }

    public void Update(
        string name,
        string description,
        decimal price,
        string imageUrl,
        MenuCategory category)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        Category = category;
    }
}