using Sag.Framework.EntityFramework.Repositories.Interfaces;
using Sag.Framework.EntityFramework.Specifications;
using Sag.Service.Vacancies.Application.Interfaces;
using Sag.Service.Vacancies.Domain;
using Microsoft.Extensions.Logging;

namespace Sag.Service.Vacancies.Application.Services
{
    public class UpdateCompanyEventService : IUpdateCompanyEventService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly ILogger<UpdateCompanyEventService> _logger;

        public UpdateCompanyEventService(IRepository<Company> companyRepository, ILogger<UpdateCompanyEventService> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task UpdateCompanyEventAsync(Guid companyId, string companyName, string? companyDisplayName, CancellationToken cancellationToken)
        {
            var existingCompany = await _companyRepository.FindOneAsync(new WithId<Company>(companyId), cancellationToken);

            if (existingCompany == null)
            {
                _logger.LogInformation("Company with id: {CompanyId} not found, no updates performed", companyId);
                return;
            }

            existingCompany.Name = companyName;
            existingCompany.DisplayName = companyDisplayName;

            await _companyRepository.UpdateAsync(existingCompany, cancellationToken: cancellationToken);
            _logger.LogInformation("Company with id: {CompanyId} updated", companyId);
        }
    }
}
