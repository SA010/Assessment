using Sag.Service.Companies.Application.Constants;

namespace Sag.Service.Companies.Application.Validators
{
    public class AddressDtoValidator : SagAbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(address => address.PostalCode)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .Matches(@"^[1-9][0-9]{3} ?(?!sa|sd|ss)[A-Za-z]{2}$")
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidPostalCode)
                .WithMessage(ErrorCode.HttpStatus400.InvalidPostalCodeMessage)
                .MaximumLength(10)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(address => address.HouseNumber)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .Matches(@"[\-0-9]{1,50}")
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidValue)
                .WithMessage(ErrorCode.HttpStatus400.InvalidValueMessage)
                .MaximumLength(50)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(address => address.Affix)
                .MaximumLength(10)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(address => address.LocationName)
                .MaximumLength(100)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(address => address.Street)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(address => address.City)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .MaximumLength(250)
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidLength)
                .WithMessage(ErrorCode.HttpStatus400.InvalidLengthMessage);

            RuleFor(address => address.Type)
                .NotNull()
                .WithErrorCode(ErrorCode.HttpStatus400.RequiredValue)
                .WithMessage(ErrorCode.HttpStatus400.RequiredValueMessage)
                .IsInEnum()
                .WithErrorCode(ErrorCode.HttpStatus400.InvalidValue)
                .WithMessage(ErrorCode.HttpStatus400.InvalidValueMessage);
        }
    }
}
