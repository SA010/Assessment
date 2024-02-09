using Sag.Framework.EntityFramework.Specifications;
using Sag.Service.Vacancies.Domain;

namespace Sag.Service.Vacancies.Application.Specifications
{
    public class AllVacancies : All<Vacancy>
    {
        public AllVacancies()
        {
            AddInclude(vacancy => vacancy.Company);
        }
    }
}
