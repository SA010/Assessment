using Sag.Service.Companies.Client.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Sag.Service.Companies.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSagCompaniesClient(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddSagSettings(configuration, CompaniesSettings.SectionName, out CompaniesSettings? companysSettings)
                .AddScoped<ISagCompanyServiceClient, SagCompanyServiceClient>(svc =>
                {
                    string? endpoint = companysSettings?.Endpoint;

                    if (string.IsNullOrEmpty(endpoint))
                    {
                        throw new ArgumentNullException($"No value for setting {CompaniesSettings.SectionName}.{nameof(CompaniesSettings.Endpoint)}");
                    }

                    HttpClient? httpClient = new() { BaseAddress = new Uri(endpoint) };

                    //Set API version 1.0 as default
                    httpClient.DefaultRequestHeaders.Add("x-api-version", "1.0");

                    //Forward current bearer token
                    IHttpContextAccessor? context = svc.GetRequiredService<IHttpContextAccessor>();
                    StringValues authHeader = default;
                    bool hasAuthorizationHeader = context.HttpContext?.Request.Headers.TryGetValue("Authorization", out authHeader) ?? false;

                    if (hasAuthorizationHeader && !string.IsNullOrEmpty(authHeader))
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", authHeader.ToString());
                    }

                    return new SagCompanyServiceClient(httpClient);
                });
        }

        private static IServiceCollection AddSagSettings<TSettings>(this IServiceCollection services, IConfiguration configuration, string sectionName, out TSettings? settings) where TSettings : class
        {
            IConfigurationSection? configSection = configuration.GetSection(sectionName);

            services.Configure<TSettings>(configSection);

            settings = configSection.Get<TSettings>();

            return services;
        }

    }
}
