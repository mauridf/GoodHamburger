namespace GoodHamburger.Web.Services;

public class OrderApiService
{
    private readonly HttpClient _http;

    public OrderApiService(HttpClient http)
    {
        _http = http;
    }
}