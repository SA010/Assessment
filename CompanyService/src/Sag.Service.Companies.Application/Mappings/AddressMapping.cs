using AutoMapper;

namespace Sag.Service.Companies.Application.Mappings
{
    public class AddressMapping : Profile
    {
        public AddressMapping()
        {
            CreateMap<Address, AddressDto>();

            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Modified, opt => opt.Ignore());
        }
    }
}
