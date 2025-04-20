using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RTB.PdfBuddy.Web;
using RTB.BlazorUI.Services.DragDrop;

using RTB.BlazorUI.Services.DataNavigationService;
using RTB.BlazorUI.Services.BusyTracker;
using RTB.BlazorUI.Services.Dialog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<DragDropService>();
builder.Services.AddSingleton<DataNavigationService>();
builder.Services.AddSingleton<BusyTracker>();
builder.Services.AddSingleton<IDialogService, DialogService>();

await builder.Build().RunAsync();
