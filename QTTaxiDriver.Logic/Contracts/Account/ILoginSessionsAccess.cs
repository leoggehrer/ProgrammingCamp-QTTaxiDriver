//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QTTaxiDriver.Logic.Contracts.Account
{
    using TOutModel = Models.Account.LoginSession;

    public partial interface ILoginSessionsAccess : IDataAccess<TOutModel>
    {
    }
}
#endif
//MdEnd
