using AutoMapper;
using Sag.SampleBFF.Api.Application.Dtos;
using Sag.Service.Companies.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Sag.Service.Companies.Application.Mappings
{
    public class CompanyMapping : Profile
    {
        public CompanyMapping()
        {
            CreateMap<CompanyCreateDto, CompanyDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Modified, opt => opt.Ignore())
                .ForMember(dest => dest.ContactPersons, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.Ignore());
        }
    }
}
