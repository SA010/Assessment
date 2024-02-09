using FluentValidation;
using Sag.Framework;
using Sag.Framework.Validation.FluentValidation;
using Sag.Service.Vacancies.Models.Dtos;
using Sag.Service.Vacancies.Models.Enums;

namespace Sag.Service.Vacancies.Application.Validators
{
    public class VacancyValidator : SagAbstractValidator<VacancyDto>
    {
        public VacancyValidator()
        {
            RuleSet("deleting", Deleting);
            
            RuleFor(vacancy => vacancy.Title)
                .NotEmpty()
                .WithErrorCode(SagErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(SagErrorCode.HttpStatus400.RequiredValueMessage)
                .MaximumLength(250)
                .WithErrorCode(SagErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(SagErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(vacancy => vacancy.Location)
                .MaximumLength(250)
                .WithErrorCode(SagErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(SagErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(vacancy => vacancy.FunctionTitle)
                .MaximumLength(250)
                .WithErrorCode(SagErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(SagErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(vacancy => vacancy.JobDescription)
                .MaximumLength(4000)
                .WithErrorCode(SagErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(SagErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(vacancy => vacancy.CompanyName)
                .MaximumLength(250)
                .WithErrorCode(SagErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(SagErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(vacancy => vacancy.CompanyDisplayName)
                .MaximumLength(250)
                .WithErrorCode(SagErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(SagErrorCode.HttpStatus400.InvalidLengthMessage);

            ConditionalRequirements();
        }

        private void Deleting()
        {
            RuleFor(vacancy => vacancy.Status)
                .Equal(VacancyStatus.Draft)
                .WithErrorCode(SagErrorCode.HttpStatus400.InvalidValue)
                .WithMessage(SagErrorCode.HttpStatus400.InvalidValueMessage);
        }
        
       
        
        private void ConditionalRequirements()
        {
            When(vacancy => vacancy.EndDate != null, () =>
            {
                RuleFor(vacancy => vacancy.EndDate)
                    .GreaterThanOrEqualTo(vacancy => vacancy.StartDate)
                    .WithErrorCode(SagErrorCode.HttpStatus400.InvalidValue)
                    .WithMessage(SagErrorCode.HttpStatus400.InvalidValueMessage);
            });
        }
    }
}
