using GoodHamburger.Web;
using GoodHamburger.Web.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7100/") // Usar a url real da API
    });

builder.Services.AddMudServices();

builder.Services.AddScoped<MenuApiService>();
builder.Services.AddScoped<OrderApiService>();
builder.Services.AddScoped<CartService>();

await builder.Build().RunAsync();