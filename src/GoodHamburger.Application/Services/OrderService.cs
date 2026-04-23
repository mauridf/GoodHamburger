using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
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
            throw new DomainException("Pedido Inválido.");

        ValidateBusinessRule(request, menuItems);

        var order = new Order();

        foreach (var reqItem in request.Items)
        {
            var menu = menuItems.FirstOrDefault(x => x.Id == reqItem.MenuItemId);

            if (menu is null)
                throw new DomainException("Item de Menu não encontrado.");

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

        ValidateBusinessRule(request, menuItems);

        order.ClearItems();

        foreach (var reqItem in request.Items)
        {
            var menu = menuItems.FirstOrDefault(x => x.Id == reqItem.MenuItemId);

            if (menu is null)
                throw new DomainException("Item de Menu não encontrado.");

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
            throw new DomainException("O pedido deve conter itens.");

        if (request.Items.Any(x => x.Quantity <= 0))
            throw new DomainException("Quantidade deve ser maior que zero.");
    }

    private static void ValidateBusinessRule(
        CreateOrderRequest request,
        List<MenuItem> menuItems)
    {
        var sandwichQty = 0;
        var sideQty = 0;
        var drinkQty = 0;

        foreach (var item in request.Items)
        {
            var menu = menuItems.FirstOrDefault(x => x.Id == item.MenuItemId);

            if (menu is null)
                throw new DomainException("Item de Menu não encontrado.");

            switch (menu.Category)
            {
                case MenuCategory.Sandwich:
                    sandwichQty += item.Quantity;
                    break;

                case MenuCategory.Side:
                    sideQty += item.Quantity;
                    break;

                case MenuCategory.Drink:
                    drinkQty += item.Quantity;
                    break;
            }
        }

        if (sandwichQty > 1)
            throw new DomainException("Apenas um sanduíche permitido por pedido.");

        if (sideQty > 1)
            throw new DomainException("Apenas um acompanhamento permitido por pedido.");

        if (drinkQty > 1)
            throw new DomainException("Apenas uma bebida permitida por pedido.");
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