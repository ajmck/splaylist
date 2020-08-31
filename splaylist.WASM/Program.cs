using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using splaylist.Helpers;

namespace splaylist.WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);



            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            ConfigureServices(builder.Services);
            await builder.Build().RunAsync();
        }



        public static void ConfigureServices(IServiceCollection services)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzA1Mjc2QDMxMzgyZTMyMmUzMFdnTDJITlJWVFNBRmdMWDZyMERpRk92dDRPNXlzcCszKzVRRWFiTXhDYVU9");
            services.AddSyncfusionBlazor();

            // following shouldn't necessarily be a singleton, but useful for testing / makes it easier in client Blazor
            services.AddSingleton<Auth>();
            services.AddSingleton<API>();
            services.AddSingleton<Cache>();
        }




    }
}
