namespace QTTaxiDriver.WpfApp.Models.Base
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public static Driver Create(Logic.Models.Base.Driver entity)
        {
            return new Driver
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
