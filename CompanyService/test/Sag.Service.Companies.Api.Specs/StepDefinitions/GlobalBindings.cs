
using Microsoft.Extensions.Options;
using Sag.Service.Companies.Api.Specs.Util;
using TechTalk.SpecFlow;

namespace Sag.Service.Companies.Api.Specs.StepDefinitions
{
    [Binding]
    public static class GlobalBindings
    {
        public static CustomWebApplicationFactory Factory { get; private set; } = null!;
        public static ILogger Logger { get; private set; } = null!;
        public static HttpClient Client { get; private set; } = null!;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Factory = new CustomWebApplicationFactory();

            Logger = Factory.GetLogger();
            Client = Factory.CreateDefaultClient(new HttpLoggingHandler(new HttpClientHandler { AllowAutoRedirect = true }, Logger));
        }

        [BeforeScenario(Order = int.MinValue)]
        public static void BeforeScenarioBase()
        {
            Factory.UseDatabase(db => db.Database.EnsureCreated());
            Factory.ClearMocks();
        }

        [AfterScenario(Order = int.MaxValue)]
        public static void AfterScenarioBase()
        {
            Factory.UseDatabase(db => db.Database.EnsureDeleted());
            //After scenario's
        }
    }
}
