namespace Sag.Service.Companies.Client.Configuration
{
    public class CompaniesSettings
    {
        public const string SectionName = "CompanyService";

        public string? Endpoint { get; set; }
        public string? HealthEndpoint { get; set; }
    }
}
