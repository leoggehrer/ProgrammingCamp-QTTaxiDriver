namespace QTTaxiDriver.AspMvc.Models.App
{
    public class Drive : ModelObject
    {
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public DateTime Date { get; set; }
        public int Distance { get; set; }

        #region navigation
        public Base.Vehicle? Vehicle { get; set; }
        public Base.Driver? Driver { get; set; }
        #endregion navigation
        public static Drive Create(Logic.Models.App.Drive entity)
        {
            return new Drive
            {
                Id = entity.Id,
                VehicleId = entity.VehicleId,
                DriverId = entity.DriverId,
                Date = entity.Date,
                Distance = entity.Distance,
                Driver = entity.Driver != default ? Base.Driver.Create(entity.Driver) : default,
            };
        }
    }
}
