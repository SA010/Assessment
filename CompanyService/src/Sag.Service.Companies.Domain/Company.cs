using Sag.Framework.EntityFramework.Domain;

namespace Sag.Service.Companies.Domain
{
    public class Company : SoftDeleteEntity
    {
        public string Name { get; set; } = default!;
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ISet<ContactPerson> ContactPersons { get; set; } = new HashSet<ContactPerson>();
        public virtual ISet<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}
