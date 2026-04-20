using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuRepository _menuRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IMenuRepository menuRepository)
    {
        _orderRepository = orderRepository;
        _menuRepository = menuRepository;
    }

    public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
    {
        ValidateRequest(request);

        var ids = request.Items
            .Select(x => x.MenuItemId)
            .Distinct()
            .ToList();

        var menuItems = await _menuRepository.GetByIdsAsync(ids);

        if (!menuItems.Any())
            throw new DomainException("Invalid order.");

        var order = new Order();

        foreach (var reqItem in request.Items)
        {
            var menu = menuItems.FirstOrDefault(x => x.Id == reqItem.MenuItemId);

            if (menu is null)
                throw new DomainException("Menu item not found.");

            order.AddItem(new OrderItem(
                menu.Id,
                menu.Name,
                menu.Price,
                menu.Category,
                reqItem.Quantity));
        }

        await _orderRepository.AddAsync(order);

        return Map(order);
    }

    public async Task<List<OrderResponse>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();

        return orders.Select(Map).ToList();
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        return order is null ? null : Map(order);
    }

    public async Task<OrderResponse?> UpdateAsync(Guid id, CreateOrderRequest request)
    {
        ValidateRequest(request);

        var order = await _orderRepository.GetByIdAsync(id);

        if (order is null)
            return null;

        var ids = request.Items
            .Select(x => x.MenuItemId)
            .Distinct()
            .ToList();

        var menuItems = await _menuRepository.GetByIdsAsync(ids);

        order.ClearItems();

        foreach (var reqItem in request.Items)
        {
            var menu = menuItems.First(x => x.Id == reqItem.MenuItemId);

            order.AddItem(new OrderItem(
                menu.Id,
                menu.Name,
                menu.Price,
                menu.Category,
                reqItem.Quantity));
        }

        await _orderRepository.UpdateAsync(order);

        return Map(order);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order is null)
            return false;

        await _orderRepository.DeleteAsync(order);

        return true;
    }

    private static void ValidateRequest(CreateOrderRequest request)
    {
        if (request.Items is null || !request.Items.Any())
            throw new DomainException("Order must contain items.");

        if (request.Items.Any(x => x.Quantity <= 0))
            throw new DomainException("Quantity must be greater than zero.");
    }

    private static OrderResponse Map(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            Subtotal = order.Subtotal,
            Discount = order.Discount,
            Total = order.Total,
            Items = order.Items.Select(i => new OrderItemResponse
            {
                Name = i.Name,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                Total = i.Total,
                Category = i.Category.ToString()
            }).ToList()
        };
    }
}