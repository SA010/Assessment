namespace Sag.Service.Companies.Application.Dtos
{
    public class CompanyDto : Dto<Company>
    {
        public string Name { get; set; } = default!;
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }

        public IReadOnlyCollection<ContactPersonDto> ContactPersons { get; set; } = default!;
        public IReadOnlyCollection<AddressDto> Addresses { get; set; } = default!;

    }
}
