using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Logging;

namespace myApp
{
    public class Program
    {
        public static void Main(string[] args) =>
            // Note that we set the content root here
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build()
                .Run();
    }

    public class Startup
    {
        public Startup() { }

        public void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app, ILoggerFactory log)
        {
            // Note that we add a simple console logger here.
            // Details: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging
            log.AddConsole();

            // Note that static files middleware will use the "wwwroot" folder in
            // the content root automatically. We do not have to specify that explicitly.
            // Details: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/#content-root
            app.UseStaticFiles();

            // Just for demo purposes we add a second middleware after static files.
            app.Map("/helloworld", beautifulApp => beautifulApp.Run(
                async context => await context.Response.WriteAsync("Hello world!")));
        }
    }
}
