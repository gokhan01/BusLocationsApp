using BusLocationsApp.Helpers;
using BusLocationsApp.Helpers.Extensions;
using BusLocationsApp.Helpers.Middlewares;
using BusLocationsApp.Helpers.Utilities;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Concretes;
using BusLocationsApp.Services.Interfaces;
using Microsoft.Net.Http.Headers;
using System.Globalization;
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

AppSettings appSettings = new();
OBiletDistribusionApiOptions obiletOptions = new();
var obiletConfig = builder.Configuration.GetSection(nameof(OBiletDistribusionApiOptions));
var appSettingsConfig = builder.Configuration.GetSection(nameof(AppSettings));

obiletConfig.Bind(obiletOptions);
appSettingsConfig.Bind(appSettings);

//Options Pattern
builder.Services.Configure<OBiletDistribusionApiOptions>(obiletConfig);
builder.Services.Configure<AppSettings>(appSettingsConfig);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = appSettings.SupportedCultures.Select(sc => new CultureInfo(sc)).ToArray();

    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: appSettings.DefaultCulture, uiCulture: appSettings.DefaultCulture);
    //This is the culture the app supports for formatting numbers, dates, etc.
    options.SupportedCultures = supportedCultures;
    //This is the culture the app supports for UI strings
    options.SupportedUICultures = supportedCultures;
});

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
builder.Services.AddScoped<IBrowserInfoProvider, BrowserInfoProvider>();
builder.Services.AddScoped<IClientInfoProvider, ClientInfoProvider>();
builder.Services.AddScoped<IOBiletSessionClient, OBiletSessionClient>();
builder.Services.AddScoped<IOBiletSessionManager, OBiletSessionManager>();
builder.Services.AddScoped<IOBiletSessionProvider, OBiletSessionProvider>();
builder.Services.AddScoped<IOBiletBusLocationsClient, OBiletBusLocationsClient>();
builder.Services.AddScoped<IOBiletJourneysClient, OBiletJourneysClient>();



var app = builder.Build();

DatetimeExtensions.SetCultureInfo(appSettings.DefaultCulture);
DecimalExtensions.SetCultureInfo(appSettings.DefaultCulture);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
