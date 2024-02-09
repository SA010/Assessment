using FluentValidation.Validators;
using Sag.Service.Companies.Application.Specifications;

namespace Sag.Service.Companies.Application.Validators.Properties
{
    public static class CompanyExistsValidatorExtension
    {
        public static IRuleBuilderOptions<TDto, Guid> CompanyExists<TDto>(this IRuleBuilder<TDto, Guid> ruleBuilder, IRepository<Company> repository)
        {
            return ruleBuilder.SetAsyncValidator(new CompanyExistsValidator<TDto>(repository));
        }
    }

    public class CompanyExistsValidator<TDto> : AsyncPropertyValidator<TDto, Guid>
    {
        private readonly IRepository<Company> _repository;

        public CompanyExistsValidator(IRepository<Company> repository)
        {
            _repository = repository;
        }

        public override string Name => nameof(CompanyExistsValidator<TDto>);

        protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} is not valid.";

        public override async Task<bool> IsValidAsync(ValidationContext<TDto> context, Guid value, CancellationToken cancellation)
        {
            var company = await _repository.FindOneAsync(new CompanyWithId(value), cancellation);

            return company != null && company.IsDeleted == false;
        }
    }
}
