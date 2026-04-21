namespace GoodHamburger.Web.Models;

public class CartItemModel
{
    public Guid MenuItemId { get; set; }

    public string Name { get; set; } = "";

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Total => UnitPrice * Quantity;
}