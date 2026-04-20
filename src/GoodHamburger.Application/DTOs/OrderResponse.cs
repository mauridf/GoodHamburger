namespace GoodHamburger.Application.DTOs;

public class OrderResponse
{
    public Guid Id { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }

    public List<OrderItemResponse> Items { get; set; } = new();
}