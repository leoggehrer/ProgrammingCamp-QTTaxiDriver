using QTTaxiDriver.Logic.Entities.Base;

namespace QTTaxiDriver.Logic.Entities.App
{
    [Table("Drives", Schema = "app")]
    public class Drive : VersionEntity
    {
        public IdType VehicleId { get; set; }
        public IdType DriverId { get; set; }
        public DateTime Date { get; set; }
        public int Distance { get; set; }

        #region Navigation properties
        public Driver? Driver { get; set; }
        public Vehicle? Vehicle { get; set; }
        #endregion Navigation properties
    }
}
