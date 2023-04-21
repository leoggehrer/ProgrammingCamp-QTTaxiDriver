//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QTTaxiDriver.Logic.Contracts.Access
{
    using TOutModel = Models.Access.AccessRule;

    public partial interface IAccessRulesAccess : Contracts.IDataAccess<TOutModel>
    {
    }
}
#endif
//MdEnd
