namespace Sag.Service.Companies.Application.Specifications
{
    public class CompanyWithId : WithId<Company>
    {
        public CompanyWithId(Guid id) : base(id)
        {
            AddInclude(company => company.ContactPersons);
            AddInclude(company => company.Addresses);
        }
    }
}
