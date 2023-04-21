using QTTaxiDriver.Logic.Modules.Common;

namespace QTTaxiDriver.Logic.Entities.Base
{
    [Table("Vehicles", Schema = "base")]
    [Index(nameof(LicensePlate), IsUnique = true)]
    public class Vehicle : VersionEntity
    {
        public IdType CompanyId { get; set; }
        public DateTime ApprovalDate { get; set; }
        [MaxLength(128)]
        public string Brand { get; set; } = string.Empty;
        [MaxLength(128)]
        public string Model { get; set; } = string.Empty;
        public VehicleType Type { get; set; }
        [MaxLength(12)]
        public string LicensePlate { get; set; } = string.Empty;
        public int Mileage { get; set; }

        #region Navigation properties
        public Company? Company { get; set; }
        public List<Driver> Drivers { get; set; } = new();
        #endregion Navigation properties
    }
}
