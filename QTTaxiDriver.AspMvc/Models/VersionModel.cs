//@CodeCopy
//MdStart

namespace QTTaxiDriver.AspMvc.Models
{
    public abstract partial class VersionModel : ModelObject
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
