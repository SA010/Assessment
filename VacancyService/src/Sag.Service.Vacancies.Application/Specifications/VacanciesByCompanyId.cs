using Sag.Framework.EntityFramework.Specifications;
using Sag.Service.Vacancies.Domain;
using System.Linq.Expressions;

namespace Sag.Service.Vacancies.Application.Specifications
{
    public class VacanciesByCompanyId : SpecificationBase<Vacancy>
    {
        private readonly Guid _companyId;

        public override Expression<Func<Vacancy, bool>> Criteria => entity => entity.CompanyId == _companyId;

        public VacanciesByCompanyId(Guid companyId)
        {
            _companyId = companyId;

            AddInclude(vacancy => vacancy.Company);
        }
    }
}
