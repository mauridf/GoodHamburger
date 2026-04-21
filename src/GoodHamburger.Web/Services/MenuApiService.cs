using GoodHamburger.Web.Models;

namespace GoodHamburger.Web.Services;

public class MenuApiService
{
    private readonly HttpClient _http;

    public MenuApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<MenuItemModel>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<MenuItemModel>>("api/menu")
            ?? new();
    }

    public async Task CreateAsync(MenuItemModel item)
    {
        await _http.PostAsJsonAsync("api/menu", item);
    }

    public async Task UpdateAsync(MenuItemModel item)
    {
        await _http.PutAsJsonAsync($"api/menu/{item.Id}", item);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _http.DeleteAsync($"api/menu/{id}");
    }
}