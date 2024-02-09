using Sag.Framework.EntityFramework.Domain;
using Sag.Service.Vacancies.Models.Enums;

namespace Sag.Service.Vacancies.Domain
{
    public class Vacancy : Entity
    {
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? Location { get; set; } = default!;
        public string? FunctionTitle { get; set; }
        public JobType JobType { get; set; }
        public WorkDays WorkDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? JobDescription { get; set; }
        public VacancyStatus Status { get; set; }
    }
}
