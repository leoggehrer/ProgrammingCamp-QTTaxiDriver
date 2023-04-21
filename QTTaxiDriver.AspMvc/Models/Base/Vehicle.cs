using QTTaxiDriver.Logic.Modules.Common;

namespace QTTaxiDriver.AspMvc.Models.Base
{
    public class Vehicle : ModelObject
    {
        [Display(Name = "Firma")]
        public IdType CompanyId { get; set; }
        [Display(Name = "Zulassung")]
        public DateTime ApprovalDate { get; set; }
        [Display(Name = "Marke")]
        public string Brand { get; set; } = string.Empty;
        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty;
        [Display(Name = "Typ")]
        public VehicleType Type { get; set; }
        [Display(Name = "Nummer")]
        public string LicensePlate { get; set; } = string.Empty;
        [Display(Name = "km")]
        public int Mileage { get; set; }

        #region Navigation properties
        public Company? Company { get; set; }
        public List<Driver> Drivers { get; set; } = new();
        #endregion Navigation properties

        public static Vehicle Create(Logic.Models.Base.Vehicle entity)
        {
            return new Vehicle
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                ApprovalDate = entity.ApprovalDate,
                Brand = entity.Brand,
                Model = entity.Model,
                Type = entity.Type,
                LicensePlate = entity.LicensePlate,
                Mileage = entity.Mileage,
                Company = entity.Company != default ? Company.Create(entity.Company) : default,
                Drivers = entity.Drivers != default ? entity.Drivers.Select(e => Driver.Create(e)).ToList() : new(),
            };
        }
    }
}
