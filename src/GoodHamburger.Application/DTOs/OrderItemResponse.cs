namespace GoodHamburger.Application.DTOs;

public class OrderItemResponse
{
    public string Name { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Total { get; set; }

    public string Category { get; set; } = string.Empty;
}