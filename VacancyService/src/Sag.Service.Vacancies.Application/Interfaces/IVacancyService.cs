using Sag.Service.Vacancies.Models.Dtos;

namespace Sag.Service.Vacancies.Application.Interfaces
{
    public interface IVacancyService
    {
        Task<VacancyDto> CreateAsync(VacancyDto vacancy, CancellationToken cancellationToken);
        Task<VacancyDto> UpdateAsync(Guid id, VacancyDto vacancy, CancellationToken cancellationToken);
        Task DeleteAsync(Guid vacancyId, CancellationToken cancellationToken);
        Task<VacancyDto> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<VacancyDto>> GetAsync(CancellationToken cancellationToken);
        Task<IReadOnlyCollection<VacancyDto>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken);
    }
}
