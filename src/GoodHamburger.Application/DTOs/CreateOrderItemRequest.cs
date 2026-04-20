namespace GoodHamburger.Application.DTOs;

public class CreateOrderItemRequest
{
    public Guid MenuItemId { get; set; }

    public int Quantity { get; set; }
}