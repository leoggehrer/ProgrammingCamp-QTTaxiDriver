namespace QTTaxiDriver.Logic.Entities.Base
{
    [Table("Companies", Schema = "base")]
    [Index(nameof(Name), IsUnique = true)]
    public class Company : VersionEntity
    {
        public string Name { get; set; } = string.Empty;
        #region Navigation properties
        public List<Vehicle> Vehicles { get; set; } = new();
        #endregion Navigation properties

    }
}
