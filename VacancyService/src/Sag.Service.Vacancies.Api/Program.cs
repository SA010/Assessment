using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Sag.Service.Vacancies.Infrastructure.EntityFramework;
using Sag.Framework.Logging;
using Sag.Framework.ESB.Extensions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using Serilog;
using Serilog.Formatting.Json;

namespace Sag.Service.Vacancies.Api
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

            host.InitializeESB();

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
                 .UseSagLogging((context, configuration) => configuration
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions()
                    {
                        FailureCallback = e => Console.WriteLine("SAG  - Unable to submit event " + e.MessageTemplate + e.RenderMessage()),
                        EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                            EmitEventFailureHandling.WriteToFailureSink |
                                            EmitEventFailureHandling.RaiseCallback,
                        FailureSink = new FileSink("./failures.txt", new JsonFormatter(), null)
                    })
                 )
                .ConfigureAppConfiguration((_, builder) => { builder.AddJsonFile("appsettings.local.json", true, true); });
        }
    }
}
