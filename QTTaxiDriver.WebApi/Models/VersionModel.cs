﻿//@CodeCopy
//MdStart
using QTTaxiDriver.Logic.Contracts;
using System.ComponentModel.DataAnnotations;

namespace QTTaxiDriver.WebApi.Models
{
    /// <summary>
    /// The model with the version property.
    /// </summary>
    public abstract partial class VersionModel : ModelObject, IVersionable
    {
#if ROWVERSION_ON
        /// <summary>
        /// Row version of the entity.
        /// </summary>
        [Timestamp]
        public byte[]? RowVersion { get; set; }
#endif
    }
}
//MdEnd
