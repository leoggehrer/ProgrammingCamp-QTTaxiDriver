namespace QTTaxiDriver.AspMvc.Models.Base
{
    public class Driver : ModelObject
    {
        [Display(Name = "Fahrer")]
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
