using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RTB.PdfBuddy.Web;
using RTB.BlazorUI.Services.DragDrop;

using RTB.BlazorUI.Services.DataNavigationService;
using RTB.BlazorUI.Services.BusyTracker;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<DragDropService>();
builder.Services.AddSingleton<DataNavigationService>();
builder.Services.AddSingleton<BusyTracker>();

await builder.Build().RunAsync();
