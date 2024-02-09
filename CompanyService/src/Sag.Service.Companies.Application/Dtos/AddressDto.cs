namespace Sag.Service.Companies.Application.Dtos
{
    public class AddressDto : Dto<Address>
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
