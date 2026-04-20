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
        var menuItems = await _menuRepository.GetByIdsAsync(request.MenuItemIds);

        if (!menuItems.Any())
            throw new DomainException("Pedido inválido.");

        var order = new Order();

        foreach (var item in menuItems)
        {
            order.AddItem(new OrderItem(
                item.Id,
                item.Name,
                item.Price,
                item.Category));
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
                Price = i.UnitPrice,
                Category = i.Category.ToString()
            }).ToList()
        };
    }
}