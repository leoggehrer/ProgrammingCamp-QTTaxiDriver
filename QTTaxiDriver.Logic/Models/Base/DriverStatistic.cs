namespace QTTaxiDriver.Logic.Models.Base
{
    public class DriverStatistic
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public int TotalDistance { get; init; }
    }
}
