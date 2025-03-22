using BusLocationsApp.Helpers;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Concretes;
using BusLocationsApp.Services.Interfaces;
using Microsoft.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Options Pattern
OBiletDistribusionApiOptions obiletOptions = new();
var obiletConfig = builder.Configuration.GetSection(nameof(OBiletDistribusionApiOptions));
obiletConfig.Bind(obiletOptions);
builder.Services.Configure<OBiletDistribusionApiOptions>(obiletConfig);

// Add HttpClient for OBilet Distribusion API to the container.
builder.Services.AddHttpClient(obiletOptions.ClientName, httpClient =>
{
    string? uriString = obiletOptions.BaseUrl;

    if (string.IsNullOrEmpty(uriString))
    {
        throw new InvalidOperationException("Configuration value for 'OBiletDistribusionApiOptions:BaseUrl' is missing or empty.");
    }

    httpClient.BaseAddress = new Uri(uriString);

    // using Microsoft.Net.Http.Headers;
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, Application.Json);
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, obiletOptions.Authorization);
});

builder.Services.AddScoped<GenericHttpClient>();
builder.Services.AddScoped<IOBiletSessionClient, OBiletSessionClient>();
builder.Services.AddScoped<IOBiletSessionManager, OBiletSessionManager>();
builder.Services.AddScoped<IOBiletSessionProvider, OBiletSessionProvider>();
builder.Services.AddScoped<IOBiletBusLocationsClient, OBiletBusLocationsClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
