using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RTB.PdfBuddy.Web;
using RTB.BlazorUI.Services; // Add this using directive

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register the DragDropService as a singleton or scoped
// Singleton is often suitable for drag-drop state across the app
builder.Services.AddSingleton<DragDropService>();

await builder.Build().RunAsync();
