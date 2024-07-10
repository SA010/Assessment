using Sag.Framework.Logging;

namespace Sag.SampleBFF.Api.Api
{
    public static class Program
    {

        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Current environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

            var host = CreateHostBuilder(args).Build();

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
