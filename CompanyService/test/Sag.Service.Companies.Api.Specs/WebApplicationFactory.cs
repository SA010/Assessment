using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Sag.Service.Companies.Api.Specs.Util;
using Sag.Service.Companies.Infrastructure.EntityFramework;

namespace Sag.Service.Companies.Api.Specs
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly List<Mock> _mocks;

        public CustomWebApplicationFactory()
        {
            _mocks = new List<Mock>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Testing")
                .ConfigureLogging((_, logging) =>
                {
                    logging.Services.AddSingleton<ILoggerProvider>(_ => new XUnitLoggerProvider());
                })
                .ConfigureAppConfiguration((_, config) =>
                {
                    var projectDir = Directory.GetCurrentDirectory();
                    var configPath = Path.Combine(projectDir, "appsettings.test.json");

                    config.Sources.Clear();
                    config.AddJsonFile(configPath, false);
                })
                .ConfigureServices(services =>
                {
                    void RemoveService(Func<ServiceDescriptor, bool> predicate)
                    {
                        foreach (var serviceToRemove in services.Where(predicate).ToList())
                        {
                            services.Remove(serviceToRemove);
                        }
                    }

                    RemoveService(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.EnableSensitiveDataLogging();
                    });
                });
        }

        public void UseDatabase(Action<ApplicationDbContext> action)
        {
            UseService(action);
        }

        public TValue UseDatabase<TValue>(Func<ApplicationDbContext, TValue> action)
        {
            return UseService(action);
        }

        public void UseOptions<TOptions>(Action<TOptions> action) where TOptions : class
        {
            UseService((IOptions<TOptions> options) => action(options.Value));
        }

        public TOptions GetOptions<TOptions>() where TOptions : class
        {
            return UseService((IOptions<TOptions> options) => options.Value);
        }

        public void UseService<T>(Action<T> action) where T : notnull
        {
            using var scope = Services.CreateScope();
            action(scope.ServiceProvider.GetRequiredService<T>());
        }

        public TValue UseService<T, TValue>(Func<T, TValue> action) where T : notnull
        {
            using var scope = Services.CreateScope();
            return action(scope.ServiceProvider.GetRequiredService<T>());
        }

        public async Task UseServiceAsync<T>(Func<T, Task> action) where T : notnull
        {
            using var scope = Services.CreateScope();
            await action(scope.ServiceProvider.GetRequiredService<T>());
        }

        public async Task<TValue> UseServiceAsync<T, TValue>(Func<T, Task<TValue>> action) where T : notnull
        {
            using var scope = Services.CreateScope();
            return await action(scope.ServiceProvider.GetRequiredService<T>());
        }

        public ILogger GetLogger()
        {
            return GetLogger<CustomWebApplicationFactory>();
        }

        public ILogger GetLogger<T>()
        {
            var provider = new XUnitLoggerProvider();
            return provider.CreateLogger(typeof(T).Name);
        }

        public T GetService<T>() where T : notnull
        {
            return Services.GetRequiredService<T>();
        }

        public Mock<T> GetMock<T>() where T : class
        {
            return _mocks.OfType<Mock<T>>().Single();
        }

        public void ClearMocks()
        {
            foreach (var mock in _mocks)
            {
                mock.Invocations.Clear();
            }
        }
    }
}
