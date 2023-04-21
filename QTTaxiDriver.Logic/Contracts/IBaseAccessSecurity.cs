//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QTTaxiDriver.Logic.Contracts
{
    partial interface IBaseAccess<T>
    {
        /// <summary>
        /// Sets the authorization token.
        /// </summary>
        string SessionToken { set; }
    }
}
#endif
//MdEnd
