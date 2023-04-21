//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QTTaxiDriver.Logic.Contracts.Account
{
    using QTTaxiDriver.Logic.Entities.Account;

    public partial interface IIdentityXRole
    {
        IdType IdentityId { get; set; }
        IdType RoleId { get; set; }
        Role? Role { get; set; }
    }
}
#endif
//MdEnd
