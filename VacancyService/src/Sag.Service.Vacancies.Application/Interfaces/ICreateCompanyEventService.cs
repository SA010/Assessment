namespace Sag.Service.Vacancies.Application.Interfaces
{
    public interface ICreateCompanyEventService
    {
        Task CreateCompanyEventAsync(Guid companyId, string companyName, string? companyDisplayName, CancellationToken cancellationToken);
    }
}
