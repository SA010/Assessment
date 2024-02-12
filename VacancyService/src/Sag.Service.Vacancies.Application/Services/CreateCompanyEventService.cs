using Sag.Framework.EntityFramework.Repositories.Interfaces;
using Sag.Framework.EntityFramework.Specifications;
using Sag.Service.Vacancies.Application.Interfaces;
using Sag.Service.Vacancies.Domain;
using Microsoft.Extensions.Logging;

namespace Sag.Service.Vacancies.Application.Services
{
    public class CreateCompanyEventService : ICreateCompanyEventService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly ILogger<CreateCompanyEventService> _logger;

        public CreateCompanyEventService(IRepository<Company> companyRepository, ILogger<CreateCompanyEventService> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task CreateCompanyEventAsync(Guid companyId, string companyName, string? companyDisplayName, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                Id = companyId,
                Name = companyName,
                DisplayName = companyDisplayName
            };
        
            await _companyRepository.InsertAsync(company, cancellationToken: cancellationToken);
            _logger.LogInformation("Company with id: {CompanyId} and name: {name} inserted", companyId, companyName);
        }
    }
}
