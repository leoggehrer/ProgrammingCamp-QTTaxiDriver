namespace QTTaxiDriver.Logic.Entities.Base
{
    [Table("Drivers", Schema = "base")]
    [Index(nameof(Name), IsUnique = true)]
    public class Driver : VersionEntity
    {
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;

        #region Navigation properties
        public List<Vehicle> Vehicles { get; set; } = new();
        #endregion Navigation properties
    }
}
