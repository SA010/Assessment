using AutoMapper;

namespace Sag.Service.Companies.Application.Mappings
{
    public class CompanyMapping : Profile
    {
        public CompanyMapping()
        {
            CreateMap<Company, CompanyDto>();
 
            CreateMap<CompanyDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Modified, opt => opt.Ignore())
                .ForMember(dest => dest.ContactPersons, opt => opt.Ignore()).AfterMap((src, dest, context) => context.AddOrUpdateOrRemove(dest.ContactPersons, src.ContactPersons))
                .ForMember(dest => dest.Addresses, opt => opt.Ignore()).AfterMap((src, dest, context) => context.AddOrUpdateOrRemove(dest.Addresses, src.Addresses));
        }
    }
}
