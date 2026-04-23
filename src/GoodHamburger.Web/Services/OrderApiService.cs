using GoodHamburger.Web.Models;
using System.Net.Http.Json;

namespace GoodHamburger.Web.Services;

public class OrderApiService
{
    private readonly HttpClient _http;

    public OrderApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<OrderResponseModel?> CreateAsync(CreateOrderRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/orders", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            throw new Exception(
                string.IsNullOrWhiteSpace(error)
                    ? "Erro ao finalizar pedido."
                    : error.Replace("\"", "")
            );
        }

        return await response.Content
            .ReadFromJsonAsync<OrderResponseModel>();
    }

    public async Task<List<OrderResponseModel>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<OrderResponseModel>>("api/orders")
            ?? new();
    }
}