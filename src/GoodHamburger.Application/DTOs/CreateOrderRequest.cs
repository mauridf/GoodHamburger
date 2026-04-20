namespace GoodHamburger.Application.DTOs;

public class CreateOrderRequest
{
    public List<Guid> MenuItemIds { get; set; } = new();
}