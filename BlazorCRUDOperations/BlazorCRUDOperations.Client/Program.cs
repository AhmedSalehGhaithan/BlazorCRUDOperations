using BlazorCRUDOperations.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedLibrary.ProductRepository;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<IProductRepository, ProductServices>();
builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
await builder.Build().RunAsync();
