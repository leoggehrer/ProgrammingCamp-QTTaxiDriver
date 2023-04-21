//@CodeCopy
//MdStart

using QTTaxiDriver.Logic.Contracts;

namespace QTTaxiDriver.Logic.ServiceModels
{
    public abstract partial class VersionService : IdentityService, IVersionable
    {
        /// <summary>
        /// Row version of the entity.
        /// </summary>
        public byte[]? RowVersion { get; set; }
    }
}
//MdEnd
