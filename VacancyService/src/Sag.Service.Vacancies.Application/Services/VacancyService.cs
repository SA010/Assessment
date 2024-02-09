using FluentValidation;
using Sag.Framework.EntityFramework.CrudService.Interfaces;
using Sag.Framework.EntityFramework.Repositories.Interfaces;
using Sag.Framework.EntityFramework.Specifications;
using Sag.Framework.ESB.Enums;
using Sag.Framework.ESB.Interfaces;
using Sag.Service.Vacancies.Domain;
using Sag.Service.Vacancies.Application.Interfaces;
using Sag.Service.Vacancies.Models.Dtos;
using Sag.Framework.Exceptions;
using Sag.Framework;
using Sag.Service.Vacancies.Application.Specifications;

namespace Sag.Service.Vacancies.Application.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly ICrudService<Vacancy, VacancyDto> _vacancyCrudService;
        private readonly IRepository<Company> _companyRepository;
        private readonly IValidator<VacancyDto> _vacancyValidator;
        private readonly ITopicService _topicService;

        public VacancyService(
            ICrudService<Vacancy, VacancyDto> vacancyCrudService,
            IValidator<VacancyDto> vacancyValidator,
            ITopicService topicService, IRepository<Company> companyRepository)
        {
            _vacancyCrudService = vacancyCrudService;
            _vacancyValidator = vacancyValidator;
            _topicService = topicService;
            _companyRepository = companyRepository;
        }

        public async Task<VacancyDto> CreateAsync(
            VacancyDto vacancy,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(vacancy);

            await _vacancyValidator.ValidateAndThrowAsync(vacancy, cancellationToken);

            await CreateCompanyIfNotExistAsync(vacancy.CompanyId, vacancy.CompanyName ?? string.Empty, vacancy.CompanyDisplayName, cancellationToken);

            var createdVacancy = await _vacancyCrudService.CreateAsync(vacancy, cancellationToken);

            _topicService.SendToTopic(nameof(Vacancy), ESBAction.Create, createdVacancy);

            return createdVacancy;
        }

        private async Task CreateCompanyIfNotExistAsync(Guid companyId, string companyName, string? companyDisplayName, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.FindOneAsync(new WithId<Company>(companyId), cancellationToken);

            if (company == null)
            {
                company = new Company
                {
                    Id = companyId,
                    Name = companyName, 
                    DisplayName = companyDisplayName
                };
                await _companyRepository.InsertAsync(company, cancellationToken);
            }
        }

        public async Task DeleteAsync(Guid vacancyId, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyCrudService.GetByIdAsync(vacancyId, cancellationToken);

            await _vacancyValidator.ValidateAsync(vacancy, options =>
            {
                options.ThrowOnFailures();
                options.IncludeRuleSets("deleting");
            }, cancellationToken);

            await _vacancyCrudService.DeleteAsync(vacancy.Id, cancellationToken);
        }

        public async Task<VacancyDto> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyCrudService.GetOneAsync(new VacancyById(id), cancellationToken);

            if (vacancy == null)
            {
                throw new SagException(SagErrorCode.HttpStatus404.Default, SagErrorCode.HttpStatus404.DefaultMessage);
            }

            return vacancy;
        }

        public Task<IReadOnlyCollection<VacancyDto>> GetAsync(CancellationToken cancellationToken)
        {
            return _vacancyCrudService.GetAsync(new AllVacancies(), cancellationToken);
        }

        public Task<IReadOnlyCollection<VacancyDto>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken)
        {
            return _vacancyCrudService.GetAsync(new VacanciesByCompanyId(companyId), cancellationToken);
        }

        public async Task<VacancyDto> UpdateAsync(
           Guid id,
           VacancyDto vacancy,
           CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(vacancy);

            if (id == Guid.Empty || id != vacancy.Id)
            {
                throw new SagException(SagErrorCode.HttpStatus400.InvalidId, SagErrorCode.HttpStatus400.InvalidIdMessage);
            }

            await _vacancyValidator.ValidateAndThrowAsync(vacancy, cancellationToken);

            var updatedVacancy = await _vacancyCrudService.UpdateAsync(vacancy, cancellationToken);

            _topicService.SendToTopic(nameof(Vacancy), ESBAction.Update, updatedVacancy);

            return updatedVacancy;
        }
    }
}
