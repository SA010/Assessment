using Sag.Framework.EntityFramework.Domain;

namespace Sag.Service.Vacancies.Domain
{
    public class Company : Entity
    {
        public string Name { get; set; } = default!;
        public string? DisplayName { get; set; }
    }
}
