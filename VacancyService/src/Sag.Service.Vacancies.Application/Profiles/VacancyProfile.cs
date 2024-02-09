using AutoMapper;
using Sag.Service.Vacancies.Domain;
using Sag.Framework.Extensions;
using Sag.Service.Vacancies.Models.Dtos;

namespace Sag.Service.Vacancies.Application.Profiles
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<Vacancy, VacancyDto>()
                .ForMember(v => v.CompanyName, opt => opt.MapFrom(e => e.Company.Name))
                .ForMember(v => v.CompanyDisplayName, opt => opt.MapFrom(e => e.Company.DisplayName));

            CreateMap<VacancyDto, Vacancy>()
                .ForMember(d => d.JobDescription, opt => opt.AddTransform(d => d.SanitizeHtml()))
                .ForMember(d => d.Company, opt => opt.Ignore());
        }
    }
}
