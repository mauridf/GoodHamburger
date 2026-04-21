namespace GoodHamburger.Web.Models;

public class OrderResponseModel
{
    public Guid Id { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Discount { get; set; }

    public decimal Total { get; set; }
}