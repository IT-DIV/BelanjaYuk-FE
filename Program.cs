using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BelanjaYuk.Client;
using BelanjaYuk.Client.Services;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load appsettings.json
var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await http.GetFromJsonAsync<Dictionary<string, string>>("appsettings.json");
var apiBaseUrl = config?["ApiBaseUrl"] ?? "https://api-dev.drian.my.id";

// Configure HttpClient with API base URL
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

// Register AuthService
builder.Services.AddScoped<AuthService>();

// Register SellerService
builder.Services.AddScoped<SellerService>();

// Register CategoryService
builder.Services.AddScoped<CategoryService>();

// Register ProductService
builder.Services.AddScoped<ProductService>();

// Register CartService
builder.Services.AddScoped<CartService>();

await builder.Build().RunAsync();
