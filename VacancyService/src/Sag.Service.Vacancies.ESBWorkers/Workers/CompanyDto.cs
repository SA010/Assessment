namespace Sag.Service.Vacancies.ESBWorkers.Workers
{
    // this Class should come from Services.Companies.Models NuGet
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Name { get; set; } = default!;
        public string? DisplayName { get; set; }

    }
}
