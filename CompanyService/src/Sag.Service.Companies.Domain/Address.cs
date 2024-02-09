using Sag.Framework.EntityFramework.Domain;
using Sag.Service.Companies.Domain.Enums;

namespace Sag.Service.Companies.Domain
{
    public class Address : Entity
    {
        public string PostalCode { get; set; } = default!;
        public string HouseNumber { get; set; } = default!;
        public string? Affix { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string? LocationName { get; set; }
        public AddressTypes Type { get; set; }

        public Guid? ContactPersonId { get; set; }
    }
}
