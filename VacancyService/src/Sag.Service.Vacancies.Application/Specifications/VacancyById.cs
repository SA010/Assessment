using Sag.Framework.EntityFramework.Specifications;
using Sag.Service.Vacancies.Domain;
using System.Linq.Expressions;

namespace Sag.Service.Vacancies.Application.Specifications
{
    public class VacancyById : SpecificationBase<Vacancy>
    {
        private readonly Guid _id;

        public override Expression<Func<Vacancy, bool>> Criteria => entity => entity.Id == _id;

        public VacancyById(Guid id)
        {
            _id = id;

            AddInclude(vacancy => vacancy.Company);
        }
    }
}
