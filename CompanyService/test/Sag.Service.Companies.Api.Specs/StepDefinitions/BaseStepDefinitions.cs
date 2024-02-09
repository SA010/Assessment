using TechTalk.SpecFlow;

namespace Sag.Service.Companies.Api.Specs.StepDefinitions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Static step definitions are not inherited")]
    public abstract partial class BaseStepDefinitions
    {
        protected CustomWebApplicationFactory Factory => GlobalBindings.Factory;
        protected ILogger Logger => GlobalBindings.Logger;

        [When(@"I initially startup the application")]
        public async Task OnStartupAsync()
        {
            await Client.GetAsync("/");
        }
    }
}
