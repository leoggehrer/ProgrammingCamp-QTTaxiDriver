using QTTaxiDriver.Logic.Modules.Common;

namespace QTTaxiDriver.ConApp
{
    partial class Program
    {
        static string FileName = "TaxiDriver.csv";
        static partial void AfterRun()
        {
            Task.Run(async () => await CreateDataByFileAsync(FileName)).Wait();
        }

        static async Task CreateDataByFileAsync(string fileName)
        {
            var lines = File.ReadAllLines(fileName, System.Text.Encoding.Default)
                            .Skip(1)
                            .Split(";");
            var companies = lines.GroupBy(l => l[6])        // CompanyName
                                 .Select(g => new Logic.Models.Base.Company
                                 {
                                     Name = g.Key,
                                 }).ToArray();
            var drivers = lines.GroupBy(l => l[7])          // DriverName
                                .Select(g => new Logic.Models.Base.Driver
                                {
                                    Name = g.Key,
                                }).ToArray();
            var licensePlates = lines.GroupBy(l => l[4]).ToArray(); // LicensePlate
            var vehicleEntities = licensePlates
                                .Select(g => new Logic.Models.Base.Vehicle
                                {
                                    ApprovalDate = DateTime.Parse(g.First()[0]),
                                    Brand = g.First()[1],
                                    Model = g.First()[2],
                                    Type = (VehicleType)Enum.Parse<VehicleType>(g.First()[3]),
                                    LicensePlate = g.First()[4],
                                    Mileage = Convert.ToInt32(g.First()[5]),
                                    Company = companies.FirstOrDefault(e => e.Name == g.First()[6]),
                                }).ToArray();
            foreach (var vehicle in vehicleEntities)
            {
                var driverNames = licensePlates.Where(e => e.Key == vehicle.LicensePlate)
                                          .SelectMany(x => x)
                                          .Select(x => x[7]) // DriverName
                                          .Distinct()
                                          .ToArray();

                foreach (var name in driverNames)
                {
                    vehicle.Drivers.Add(drivers.First(d => d.Name == name));
                }
            }

            using var vehiclesAccess = new Logic.Controllers.Base.VehiclesController();

            var insertVehicles = await vehiclesAccess.InsertAsync(vehicleEntities);
            await vehiclesAccess.SaveChangesAsync();

            var drives = new List<Logic.Models.App.Drive>();

            foreach (var vehicle in insertVehicles)
            {
                foreach (var driver in vehicle.Drivers)
                {
                    var driveData = lines.Where(l => l[4] == vehicle.LicensePlate && l[7] == driver.Name).ToArray();

                    foreach (var drive in driveData)
                    {
                        drives.Add(new Logic.Models.App.Drive
                        {
                            VehicleId = vehicle.Id,
                            DriverId = driver.Id,
                            Date = DateTime.Parse(drive[8]),
                            Distance = Int32.Parse(drive[9]),
                        });
                    }
                }
            }

            using var drivesCtrl = new Logic.Controllers.App.DrivesController();

            await drivesCtrl.InsertAsync(drives);
            await drivesCtrl.SaveChangesAsync();
        }
    }
}
