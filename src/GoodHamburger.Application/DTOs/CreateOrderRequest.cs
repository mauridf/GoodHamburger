namespace GoodHamburger.Application.DTOs;

public class CreateOrderRequest
{
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}