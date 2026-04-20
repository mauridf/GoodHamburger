using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.DTOs;

public class CreateMenuItemRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public MenuCategory Category { get; set; }
}