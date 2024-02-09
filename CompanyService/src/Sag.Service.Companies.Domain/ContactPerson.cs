using Sag.Framework.EntityFramework.Domain;

namespace Sag.Service.Companies.Domain
{
    public class ContactPerson : Entity
    {
        public string FirstName { get; set; } = default!;
        public string? Preposition { get; set; }
        public string LastName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? FunctionTitle { get; set; }
        public bool IsMainContactPerson { get; set; }
    }
}
