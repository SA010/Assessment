using Sag.Framework.ESB.Enums;
using Sag.Framework.Exceptions;
using Sag.Service.Companies.Application.Constants;
using Sag.Service.Companies.Application.EventHandlers;
using Sag.Service.Companies.Application.Services.Interfaces;
using Sag.Service.Companies.Application.Validators.Generic.Interfaces;

namespace Sag.Service.Companies.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICrudService<Company, CompanyDto> _companyCrudService;
        private readonly IGenericValidator _validator;
        private readonly IEventHandler<Company, CompanyDto> _companyEventHandler;

        public CompanyService(
            ICrudService<Company, CompanyDto> companyCrudService,
            IGenericValidator validator, IRepository<Company> companyRepository,
            IEventHandler<Company, CompanyDto> companyEventHandler)
        {
            _companyCrudService = companyCrudService;
            _validator = validator;
            _companyEventHandler = companyEventHandler;
        }

        public async Task<CompanyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var company = await _companyCrudService.GetByIdAsync(id, cancellationToken);

            if (company == null)
            {
                throw new SagException(ErrorCode.HttpStatus404.CompanyNotFound, ErrorCode.HttpStatus404.CompanyNotFoundMessage);
            }

            return company;
        }

        public async Task<CompanyDto> CreateAsync(CompanyDto input, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(input, cancellationToken);

            var company = await _companyCrudService.CreateAsync(input, cancellationToken);
            _companyEventHandler.Publish(company, ESBAction.Create);
            return company;
        }

        public async Task<CompanyDto> UpdateAsync(CompanyDto input, CancellationToken cancellationToken)
        {

            await _validator.ValidateAndThrowAsync(input, cancellationToken);

            var company = await _companyCrudService.GetByIdAsync(input.Id, cancellationToken);
            if (company == null)
            {
                throw new SagException(ErrorCode.HttpStatus404.CompanyNotFound, ErrorCode.HttpStatus404.CompanyNotFoundMessage);
            }

            var updatedCompany = await _companyCrudService.UpdateAsync(input, cancellationToken);
            _companyEventHandler.Publish(updatedCompany, ESBAction.Update);
            return updatedCompany;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var company = await _companyCrudService.GetByIdAsync(id, cancellationToken);

            if (company == null)
            {
                throw new SagException(ErrorCode.HttpStatus404.CompanyNotFound, ErrorCode.HttpStatus404.CompanyNotFoundMessage);
            }

            await _companyCrudService.DeleteAsync(id, cancellationToken);
            _companyEventHandler.Publish(company, ESBAction.Delete);
        }

        public async Task<IReadOnlyCollection<CompanyDto>> GetAsync(CancellationToken cancellationToken)
        {
            return await _companyCrudService.GetAsync(new All<Company>() ,cancellationToken);
        }
    }
}
