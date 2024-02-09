namespace Sag.Service.Companies.Application.Constants
{
    public static class ErrorCode
    {
        public class HttpStatus400 : SagErrorCode.HttpStatus400
        {
            public const string TooManyBillingAddresses = "400.7.1";
            public const string TooManyBillingAddressesMessage = "Only one billing address is allowed";

            public const string TooManyVisitingAddresses = "400.7.2";
            public const string TooManyVisitingAddressesMessage = "Only one visiting address is allowed";

            public const string TooManyMainContactPersons = "400.7.3";
            public const string TooManyMainContactPersonsMessage = "Only one main contact person is allowed";
        }

        public class HttpStatus404 : SagErrorCode.HttpStatus404
        {
            public new const string CompanyNotFound = "404.7.1";
            public new const string CompanyNotFoundMessage = "Company not found";
        }
    }
}
