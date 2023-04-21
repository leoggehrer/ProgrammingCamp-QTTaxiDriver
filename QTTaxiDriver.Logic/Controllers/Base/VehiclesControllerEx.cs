﻿namespace QTTaxiDriver.Logic.Controllers.Base
{
    partial class VehiclesController
    {
        internal override IEnumerable<string> Includes => new string[] { nameof(Entities.Base.Vehicle.Company), nameof(Entities.Base.Vehicle.Drivers) };
        public Task<Models.Base.Vehicle[]> QueryByAsync(string? type, string? companyOrBrand)
        {
            var query = EntitySet.AsQueryable();
            Modules.Common.VehicleType vt;

            query = query.Include(e => e.Company)
                         .Include(e => e.Drivers);

            if (string.IsNullOrEmpty(type) == false 
                && Enum.TryParse<Modules.Common.VehicleType>(type, out vt))
            {
                query = query.Where(e => e.Type == vt);
            }

            if (string.IsNullOrEmpty(companyOrBrand) == false)
            {
                query = query.Where(e => e.Company!.Name.ToUpper().Contains(companyOrBrand.ToUpper())
                                      || e.Brand.ToUpper().Contains(companyOrBrand.ToUpper()));
            }
            return query.Select(e => new Models.Base.Vehicle(e)).ToArrayAsync();
        }

        public async Task<Models.Base.VehicleStatistics> CalculateStatisticsAsync(int id)
        {
            var result = new Models.Base.VehicleStatistics { Id = id };
            var vehicle = await ExecuteGetByIdAsync(id).ConfigureAwait(false);

            if (vehicle != default)
            {
                using var drivesCtrl = new App.DrivesController(this);
                var drives = await drivesCtrl.ExecuteQueryAsync(e => e.VehicleId == id).ConfigureAwait(false);

                result.CompanyName = vehicle.Company?.Name;
                result.Brand = vehicle.Brand;
                result.Model = vehicle.Model;
                result.LicensPlate = vehicle.LicensePlate;
                result.TotalDistance = drives.Select(d => d.Distance)
                                             .DefaultIfEmpty(0)
                                             .Sum();
                result.AverageDistance = vehicle.Drivers.Any() ? result.TotalDistance / vehicle.Drivers.Count : 0;

                var driverStatistics = new List<Models.Base.DriverStatistic>();

                foreach (var driver in vehicle.Drivers)
                {
                    driverStatistics.Add(new Models.Base.DriverStatistic
                    {
                        Id = driver.Id,
                        Name = driver.Name,
                        TotalDistance = drives.Where(e => e.DriverId == driver.Id)
                                              .Select(e => e.Distance)
                                              .DefaultIfEmpty(0)
                                              .Sum(),
                    });
                }
                result.DriverStatistics = driverStatistics.ToArray();
            }
            return result;
        }
    }
}
