using Sag.Framework.ESB.Extensions;
using Sag.Framework.Logging;
using Sag.Service.Vacancies.Application;
using Sag.Service.Vacancies.ESBWorkers.Workers;
using Sag.Service.Vacancies.Infrastructure.EntityFramework;
using System.Reflection;
using Sag.Framework.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Sag.Service.Vacancies.Application.Interfaces;
using Sag.Service.Vacancies.Application.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Claims;

namespace Sag.Service.Vacancies.ESBWorkers
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
                .ConfigureServices((context, services) =>
                {
                    Console.WriteLine($"ESB config: {context.Configuration["ESBSettings:EndpointHost"]}");
                    services.AddSagESB(context.Configuration);
                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).GetTypeInfo().Assembly, typeof(Anchor).GetTypeInfo().Assembly));
                    services.TryAddTransient(_ => new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>())));

                    services.AddSagDatabase<ApplicationDbContext>(options => options.UseSqlServer(
                        context.Configuration.GetConnectionString("SagVacancy"),
                        sqlServerOptions => sqlServerOptions
                            .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                            .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                            .UseNetTopologySuite()
                    ));
                    services.AddAutoMapper(typeof(Program).Assembly, typeof(Anchor).Assembly);

                    services.AddScoped<ICreateCompanyEventService, CreateCompanyEventService>();
                    services.AddScoped<IUpdateCompanyEventService, UpdateCompanyEventService>();

                    AddWorkers(services);
                })
                .ConfigureAppConfiguration((_, builder) =>
                {
                    builder.AddJsonFile("appsettings.local.json", true, true);

                })
                .UseSagLogging();
        }

        private static void AddWorkers(IServiceCollection services)
        {
           services.AddHostedService<CompanyCreateWorker>();
           services.AddHostedService<CompanyUpdateWorker>();
        }
    }
}
