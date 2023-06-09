﻿//@GeneratedCode
namespace QTTaxiDriver.MvvMApp.Models.Base
{
    using System;
    ///
    /// Generated by the generator
    ///
    
    public partial class Company
    {
        ///
        /// Generated by the generator
        ///
        static Company()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        ///
        /// Generated by the generator
        ///
        public Company()
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        
        public System.String Name { get; set; } = string.Empty;
        ///
        /// Generated by the generator
        ///
        
        public System.Collections.Generic.List<QTTaxiDriver.MvvMApp.Models.Base.Vehicle> Vehicles { get; set; } = new();
        ///
        /// Generated by the generator
        ///
        public static QTTaxiDriver.MvvMApp.Models.Base.Company Create()
        {
            BeforeCreate();
            var result = new QTTaxiDriver.MvvMApp.Models.Base.Company();
            AfterCreate(result);
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public static QTTaxiDriver.MvvMApp.Models.Base.Company Create(object other)
        {
            BeforeCreate(other);
            var result = new QTTaxiDriver.MvvMApp.Models.Base.Company();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QTTaxiDriver.MvvMApp.Models.Base.Company instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QTTaxiDriver.MvvMApp.Models.Base.Company instance, object other);
        ///
        /// Generated by the generator
        ///
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Base.Company other)
            {
                result = Id == other.Id;
            }
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public override int GetHashCode()
        {
            return this.CalculateHashCode(Name, Vehicles, Id);
        }
    }
}
