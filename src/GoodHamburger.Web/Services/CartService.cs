using GoodHamburger.Web.Models;

namespace GoodHamburger.Web.Services;

public class CartService
{
    public event Action? OnChange;

    public List<CartItemModel> Items { get; private set; } = new();

    public decimal Subtotal => Items.Sum(x => x.Total);

    public void Add(MenuItemModel item)
    {
        var existing = Items.FirstOrDefault(x => x.MenuItemId == item.Id);

        if (existing is null)
        {
            Items.Add(new CartItemModel
            {
                MenuItemId = item.Id,
                Name = item.Name,
                UnitPrice = item.Price,
                Quantity = 1
            });
        }
        else
        {
            existing.Quantity++;
        }

        Notify();
    }

    public void Increase(Guid id)
    {
        var item = Items.First(x => x.MenuItemId == id);
        item.Quantity++;
        Notify();
    }

    public void Decrease(Guid id)
    {
        var item = Items.First(x => x.MenuItemId == id);

        item.Quantity--;

        if (item.Quantity <= 0)
            Items.Remove(item);

        Notify();
    }

    public void Clear()
    {
        Items.Clear();
        Notify();
    }

    private void Notify() => OnChange?.Invoke();
}