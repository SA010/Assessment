using AutoMapper;

namespace Sag.Service.Companies.Application.Mappings
{
    public class ContactPersonMapping : Profile
    {
        public ContactPersonMapping()
        {
            CreateMap<ContactPerson, ContactPersonDto>();

            CreateMap<ContactPersonDto, ContactPerson>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Modified, opt => opt.Ignore());
        }
    }
}
