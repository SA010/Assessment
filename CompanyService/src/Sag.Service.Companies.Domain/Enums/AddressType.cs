namespace Sag.Service.Companies.Domain.Enums
{
    [Flags]
    public enum AddressTypes
    {
        None = 0,
        Billing = 1 << 0,
        Visiting = 1 << 1,
        Working = 1 << 2,
        All = Billing | Visiting | Working
    }
}
