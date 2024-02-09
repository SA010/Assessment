using Sag.Framework.Application.Common.Interfaces;
using Sag.Service.Vacancies.Models.Enums;

namespace Sag.Service.Vacancies.Models.Dtos
{
    public class VacancyDto : IDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid CompanyId { get; set; }
        public string Title { get; set; } = default!;
        public string? Location { get; set; }
        public string? FunctionTitle { get; set; }
        public JobType JobType { get; set; }
        public WorkDays WorkDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? JobDescription { get; set; }
        public VacancyStatus Status { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyDisplayName { get; set; }
    }
}
