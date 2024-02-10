using Sag.Framework.Validation.Extensions;
using Sag.Service.Companies.Application.Constants;
using Sag.Service.Companies.Application.Validators.Properties;

namespace Sag.Service.Companies.Application.Validators
{
    public class CompanyDtoValidator : SagAbstractValidator<CompanyDto>
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IValidator<AddressDto> _addressValidator;
        private readonly IValidator<ContactPersonDto> _contactPersonValidator;

        public CompanyDtoValidator(
            IRepository<Company> companyRepository,
            IValidator<AddressDto> addressValidator,
            IValidator<ContactPersonDto> contactPersonValidator)
        {
            _companyRepository = companyRepository;
            _addressValidator = addressValidator;
            _contactPersonValidator = contactPersonValidator;

            ValidateProperties();
            ValidationConnections();
        }

        private void ValidateProperties()
        {
            RuleFor(company => company.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(company => company.EmailAddress)
                .EmailAddress()
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidValue)
                .WithMessage(ErrorCode.HttpStatus400.InvalidValueMessage)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(company => company.EmailAddress)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage)
                .EmailAddress()
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidEmailAddress)
                .WithMessage(ErrorCode.HttpStatus400.InvalidEmailAddressMessage);

            RuleFor(company => company.PhoneNumber)
                .MaximumLength(25)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage)
                .SagPhoneNumber()
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidPhoneNumber)
                .WithMessage(ErrorCode.HttpStatus400.InvalidPhoneNumberMessage);
        }

        private void ValidationConnections()
        {

        //    When(company => company.Addresses != null && company.Addresses.Any(), () =>
        //    {
        //        RuleFor(company => company.Addresses)
        //            .Must(addresses => addresses.Count(address => address.Type.HasFlag(AddressTypes.Billing)) <= 1)
        //            .WithErrorCode(ErrorCode.HttpStatus400.TooManyBillingAddresses)
        //            .WithMessage(ErrorCode.HttpStatus400.TooManyBillingAddressesMessage)
        //            .Must(addresses => addresses.Count(address => address.Type.HasFlag(AddressTypes.Visiting)) <= 1)
        //            .WithErrorCode(ErrorCode.HttpStatus400.TooManyVisitingAddresses)
        //            .WithMessage(ErrorCode.HttpStatus400.TooManyVisitingAddressesMessage);

        //        RuleForEach(company => company.Addresses)
        //            .SetValidator(_addressValidator);
        //    });

        //    When(company => company.ContactPersons != null && company.ContactPersons.Any(), () =>
        //    {
        //        RuleFor(company => company.ContactPersons)
        //            .Must(contactPersons => contactPersons.Count(contactPerson => contactPerson.IsMainContactPerson) <= 1)
        //            .WithErrorCode(ErrorCode.HttpStatus400.TooManyMainContactPersons)
        //            .WithMessage(ErrorCode.HttpStatus400.TooManyMainContactPersonsMessage);

        //        RuleForEach(company => company.ContactPersons)
        //            .SetValidator(_contactPersonValidator);
        //    });
        }
    }
}
