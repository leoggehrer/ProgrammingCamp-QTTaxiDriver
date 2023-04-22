using QTTaxiDriver.Logic.Modules.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTTaxiDriver.WpfApp.Models.Base
{
    public class Vehicle
    {
        public int Id { get; set; }
        public IdType CompanyId { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public VehicleType Type { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public int Mileage { get; set; }


        public string CompanyName => Company != default ? Company.Name : string.Empty;

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
