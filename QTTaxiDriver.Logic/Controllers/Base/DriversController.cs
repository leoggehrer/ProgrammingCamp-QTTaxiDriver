﻿//@GeneratedCode
namespace QTTaxiDriver.Logic.Controllers.Base
{
    using TEntity = Entities.Base.Driver;
    using TOutModel = Models.Base.Driver;
    ///
    /// Generated by the generator
    ///
    
    public sealed partial class DriversController : EntitiesController<TEntity, TOutModel>, Contracts.Base.IDriversAccess
    {
        ///
        /// Generated by the generator
        ///
        static DriversController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        ///
        /// Generated by the generator
        ///
        public DriversController()
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        public DriversController(ControllerObject other)
        : base(other)
        {
            Constructing();
            
            Constructed();
        }
        ///
        /// Generated by the generator
        ///
        internal override TOutModel ToModel(TEntity entity)
        {
            var handled = false;
            TOutModel? result = default;
            
            BeforeToOutModel(entity, ref result, ref handled);
            if (handled == false || result == default)
            {
                result = new TOutModel(entity);
            }
            AfterToOutModel(entity, result);
            return result;
        }
        partial void BeforeToOutModel(TEntity entity, ref TOutModel? result, ref bool handled);
        partial void AfterToOutModel(TEntity entity, TOutModel result);
    }
}
