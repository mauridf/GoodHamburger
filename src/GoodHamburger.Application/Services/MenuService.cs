using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.Services;

public class MenuService
{
    private readonly IMenuRepository _repository;

    public MenuService(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MenuItemResponse>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();

        return items.Select(Map).ToList();
    }

    public async Task<MenuItemResponse?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);

        return item is null ? null : Map(item);
    }

    public async Task<MenuItemResponse> CreateAsync(CreateMenuItemRequest request)
    {
        var item = new MenuItem(
            Guid.NewGuid(),
            request.Name,
            request.Price,
            (MenuCategory)request.Category);

        await _repository.AddAsync(item);

        return Map(item);
    }

    public async Task<MenuItemResponse?> UpdateAsync(Guid id, UpdateMenuItemRequest request)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item is null)
            return null;

        item.Update(
            request.Name,
            request.Price,
            (MenuCategory)request.Category);

        await _repository.UpdateAsync(item);

        return Map(item);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item is null)
            return false;

        await _repository.DeleteAsync(item);

        return true;
    }

    private static MenuItemResponse Map(MenuItem item)
    {
        return new MenuItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            Category = item.Category.ToString()
        };
    }
}