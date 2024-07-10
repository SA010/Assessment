
namespace Sag.Service.Companies.Application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IReadOnlyCollection<CompanyDto>> GetAsync(CancellationToken cancellationToken);
        Task<CompanyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<CompanyDto> CreateAsync(CompanyDto input, CancellationToken cancellationToken);

        Task<CompanyDto> UpdateAsync(CompanyDto input, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
