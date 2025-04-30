using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RTB.PdfBuddy.Web;
using RTB.BlazorUI.Services.DragDrop;

using RTB.BlazorUI.Services.DataNavigationService;
using RTB.BlazorUI.Services.BusyTracker;
using RTB.BlazorUI.Services.Dialog;
using System.Runtime.CompilerServices;
using RTB.PdfBuddy.Web.Components;
using RTB.PdfBuddy.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IDragDropService, DragDropService>();
builder.Services.AddSingleton<DataNavigationService>();
builder.Services.AddSingleton<IBusyTracker, BusyTracker>();
builder.Services.AddSingleton<IDialogService, DialogService>();
builder.Services.AddSingleton<PdfEditor>();


var app = builder.Build();

await app.RunAsync();
