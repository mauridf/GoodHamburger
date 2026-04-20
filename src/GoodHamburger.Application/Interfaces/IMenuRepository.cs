using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface IMenuRepository
{
    Task<List<MenuItem>> GetAllAsync();
    Task<List<MenuItem>> GetByIdsAsync(List<Guid> ids);
}