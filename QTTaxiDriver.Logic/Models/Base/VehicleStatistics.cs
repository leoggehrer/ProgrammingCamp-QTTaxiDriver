namespace QTTaxiDriver.Logic.Models.Base
{
    public class VehicleStatistics
    {
        public int Id { get; init; }
        public string? CompanyName { get; internal set; }
        public string? Brand { get; internal set; }
        public string? Model { get; internal set; }
        public string? LicensPlate { get; internal set; }
        public int TotalDistance { get; internal set; }
        public double AverageDistance { get; internal set; }

        public DriverStatistic[] DriverStatistics { get; internal set; } = Array.Empty<DriverStatistic>();
    }
}
