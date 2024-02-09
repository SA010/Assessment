using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Sag.Service.Companies.Infrastructure.EntityFramework;
using Sag.Framework.Logging;

namespace Sag.Service.Companies.Api
{
    public static class Program
    {
        private static readonly RetryPolicy RetryPolicy = Policy.Handle<Exception>().WaitAndRetry(5, _ => TimeSpan.FromSeconds(5));

        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Current environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                RetryPolicy.Execute(context.Database.Migrate);
            }

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureKestrel(options => options.AddServerHeader = false)
                        .UseStartup<Startup>();
                })
                .UseSagLogging()
                .ConfigureAppConfiguration((_, builder) => { builder.AddJsonFile("appsettings.local.json", true, true); });
        }
    }
}
