using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NoteManager.Client.Components.Toast;
using NoteManager.Client.Services;

namespace NoteManager.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7200") });
            builder.Services.AddScoped<HttpInterceptorService>();
            builder.Services.AddScoped<ToastService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
            await builder.Build().RunAsync();
        }
    }
}
