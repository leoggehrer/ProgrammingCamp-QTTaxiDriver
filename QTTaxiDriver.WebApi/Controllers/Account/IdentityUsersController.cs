﻿//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QTTaxiDriver.WebApi.Controllers.Account
{
    using TAccessModel = Logic.Models.Account.User;
    using TEditModel = WebApi.Models.Account.IdentityUserEdit;
    using TOutModel = WebApi.Models.Account.IdentityUser;
    ///
    /// Generated by the generator
    ///
    public sealed partial class IdentityUsersController : Controllers.GenericController<TAccessModel, TEditModel, TOutModel>
    {
        ///
        /// Generated by the generator
        ///
        static IdentityUsersController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        ///
        /// Generated by the generator
        ///
        public IdentityUsersController(Logic.Contracts.Account.IUsersAccess other)
        : base(other)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        protected override TOutModel ToOutModel(TAccessModel accessModel)
        {
            var handled = false;
            var result = default(TOutModel);
            BeforeToOutModel(accessModel, ref result, ref handled);
            if (handled == false || result == null)
            {
                result = TOutModel.Create(accessModel);
            }
            AfterToOutModel(result);
            return result;
        }
        partial void BeforeToOutModel(TAccessModel accessModel, ref TOutModel? outModel, ref bool handled);
        partial void AfterToOutModel(TOutModel outModel);
    }
}
#endif
//MdEnd
