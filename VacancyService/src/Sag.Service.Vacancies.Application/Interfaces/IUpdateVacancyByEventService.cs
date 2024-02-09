namespace Sag.Service.Vacancies.Application.Interfaces
{
    public interface IUpdateVacancyByEventService
    {
        Task UpdateByCompanyEventAsync(Guid companyId, string companyName, string? companyDisplayName, CancellationToken cancellationToken);
    }
}
