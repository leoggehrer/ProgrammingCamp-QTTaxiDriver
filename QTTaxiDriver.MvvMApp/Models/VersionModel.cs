//@CodeCopy
//MdStart
using System;
namespace QTTaxiDriver.MvvMApp.Models
{
	public abstract partial class VersionModel : ModelObject, Logic.Contracts.IVersionable
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
