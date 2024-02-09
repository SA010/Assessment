using Sag.Framework.Validation.Extensions;
using Sag.Service.Companies.Application.Constants;

namespace Sag.Service.Companies.Application.Validators
{
    public class ContactPersonDtoValidator : SagAbstractValidator<ContactPersonDto>
    {
        public ContactPersonDtoValidator()
        {
            RuleFor(contactPerson => contactPerson.FirstName)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(contactPerson => contactPerson.Preposition)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(contactPerson => contactPerson.LastName)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(contactPerson => contactPerson.EmailAddress)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage)
                .EmailAddress()
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidEmailAddress)
                .WithMessage(ErrorCode.HttpStatus400.InvalidEmailAddressMessage);

            RuleFor(contactPerson => contactPerson.PhoneNumber)
                .MaximumLength(25)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage)
                .SagPhoneNumber()
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidPhoneNumber)
                .WithMessage(ErrorCode.HttpStatus400.InvalidPhoneNumberMessage);

            RuleFor(contactPerson => contactPerson.FunctionTitle)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);
        }
    }
}
