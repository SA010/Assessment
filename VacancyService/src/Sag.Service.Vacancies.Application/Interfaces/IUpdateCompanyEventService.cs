namespace Sag.Service.Vacancies.Application.Interfaces
{
    public interface IUpdateCompanyEventService
    {
        Task UpdateCompanyEventAsync(Guid companyId, string companyName, string? companyDisplayName, CancellationToken cancellationToken);
    }
}
