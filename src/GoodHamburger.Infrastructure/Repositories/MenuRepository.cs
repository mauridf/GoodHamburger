using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly AppDbContext _context;

    public MenuRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(MenuItem item)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(MenuItem item)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItems.ToListAsync();
    }

    public Task<MenuItem?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MenuItem>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.MenuItems
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public Task UpdateAsync(MenuItem item)
    {
        throw new NotImplementedException();
    }
}