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
}