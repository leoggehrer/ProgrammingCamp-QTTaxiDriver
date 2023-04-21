//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QTTaxiDriver.Logic.Contracts.Account
{
    using QTTaxiDriver.Logic.Modules.Common;

    public partial interface IIdentity
    {
        IdType Id { get; }
        Guid Guid { get; }
        string Name { get; set; }
        string Email { get; set; }
        int TimeOutInMinutes { get; set; }
        int AccessFailedCount { get; set; }
        State State { get; set; }
        IRole[] Roles { get; }

        bool HasRole(Guid guid);
    }
}
#endif
//MdEnd
