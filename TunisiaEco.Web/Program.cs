using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TunisiaEco.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Update to use the correct API port (5161)
builder.Services.AddScoped(sp => 
    new HttpClient { 
        BaseAddress = new Uri("http://localhost:5161") 
    });

await builder.Build().RunAsync();