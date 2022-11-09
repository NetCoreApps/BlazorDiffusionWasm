using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Blazor.Extensions.Logging;
using ServiceStack.Blazor;
using BlazorDiffusion;
using BlazorDiffusion.UI;
using Ljbc1994.Blazor.IntersectionObserver;
using BlazorDiffusion.ServiceModel;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddLogging(c => c
    .AddBrowserConsole()
    .SetMinimumLevel(LogLevel.Trace)
);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

if (builder.HostEnvironment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Information);
}

// Use / for local or CDN resources
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;
builder.Services.AddBlazorApiClient(apiBaseUrl);
builder.Services.AddScoped<ServiceStackStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ServiceStackStateProvider>());

builder.Services.AddScoped<KeyboardNavigation>();
builder.Services.AddScoped<UserState>();
builder.Services.AddIntersectionObserver();

var app = builder.Build();

BlazorConfig.Set(new BlazorConfig
{
    IsWasm = true,
    Services = app.Services,
    AssetsBasePath = "https://cdn.diffusion.works",
    FallbackAssetsBasePath = "https://pub-97bba6b94a944260b10a6e7d4bf98053.r2.dev",
    EnableLogging = true,
    EnableVerboseLogging = builder.HostEnvironment.IsDevelopment(),
    DefaultProfileUrl = Icons.AnonUserUri,
    OnApiErrorAsync = (request, apiError) =>
    {
        BlazorConfig.Instance.GetLog()?.LogDebug("\n\nOnApiErrorAsync(): {0}", apiError.Error.GetDetailedError());
        return Task.CompletedTask;
    }
});

await app.RunAsync();
