namespace QTTaxiDriver.AspMvc.Models.Base
{
    public class Company : ModelObject
    {
        [Display(Name = "Firma")]
        public string Name { get; set; } = string.Empty;

        public static Company Create(Logic.Models.Base.Company entity)
        {
            return new Company
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
