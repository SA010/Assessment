using AutoMapper;
using FluentAssertions;
using Sag.SampleBFF.Api.Api.Controllers;
using Sag.SampleBFF.Api.Api.Specs.StepDefinitions;
using System.Net;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;

namespace Sag.SampleBFF.Api.Api.Specs.Features.Config
{
    [Binding, Scope(Feature = "Configuration")]
    public sealed class ConfigurationStepDefinitions : BaseStepDefinitions
    {
        [Then(@"I expect that all API controllers can be instantiated correctly")]
        public void VerifyApiControllers()
        {
            var logger = Factory.GetLogger();
            var errors = new List<string>();
            foreach (var controller in typeof(Startup).Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseController))))
            {
                logger.LogInformation($"Controller {controller.FullName}");
                try
                {
                    ActivatorUtilities.CreateInstance(Factory.Services, controller);
                }
                catch (Exception e)
                {
                    errors.Add(e.Message);
                    logger.LogWarning(e, e.Message);
                }
            }

            errors.Distinct().Should().BeEmpty();
        }

        [Then(@"I expect that a swagger endpoint is available")]
        public static async Task VerifySwagger()
        {
            var htmlResponse = await Client.GetAsync("/swagger/index.html");
            htmlResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            htmlResponse.Content.Headers.ContentType.Should().NotBeNull()
                .And.BeOfType<MediaTypeHeaderValue>()
                .Which.MediaType.Should().Be("text/html");

            var jsonResponse = await Client.GetAsync("/swagger/v1/swagger.json");
            jsonResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            jsonResponse.Content.Headers.ContentType.Should().NotBeNull()
                .And.BeOfType<MediaTypeHeaderValue>()
                .Which.MediaType.Should().Be("application/json");
        }

        [Then(@"I expect that all AutoMapper profiles are configured correctly")]
        public void VerifyAutoMapper()
        {
            var logger = Factory.GetLogger();
            logger.LogInformation("AutoMapper profiles");

            var mapper = Factory.GetService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
