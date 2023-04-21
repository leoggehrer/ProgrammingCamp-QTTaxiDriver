//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QTTaxiDriver.Logic.Entities.Account
{
    using QTTaxiDriver.Logic.Contracts.Account;

#if SQLITE_ON
    [Table("Roles")]
#else
    [Table("Roles", Schema = "account")]
#endif
    [Index(nameof(Designation), IsUnique = true)]
    public partial class Role : VersionExtendedEntity, IRole
    {
        public Guid Guid { get; internal set; }
        [MaxLength(64)]
        public string Designation { get; set; } = string.Empty;
        [MaxLength(256)]
        public string? Description { get; set; }
    }
}
#endif
//MdEnd
