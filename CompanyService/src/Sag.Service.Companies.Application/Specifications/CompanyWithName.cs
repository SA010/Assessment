using System.Linq.Expressions;

namespace Sag.Service.Companies.Application.Specifications
{
    public class CompanyWithName : SpecificationBase<Company>
    {
        private readonly string _name;

        public override Expression<Func<Company, bool>> Criteria => entity => entity.IsDeleted == false && entity.Name == _name;

        public CompanyWithName(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
