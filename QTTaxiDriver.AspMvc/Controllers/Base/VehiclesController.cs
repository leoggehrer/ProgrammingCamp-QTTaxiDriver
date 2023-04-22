using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QTTaxiDriver.AspMvc.Controllers.Base
{
    using TModel = Models.Base.Vehicle;

    public class VehiclesController : Controller
    {
        private Logic.Contracts.Base.IVehiclesAccess _dataAccess;
        public VehiclesController(Logic.Contracts.Base.IVehiclesAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        // GET: VehiclesController
        public async Task<ActionResult> Index()
        {
            var entities = await _dataAccess.GetAllAsync();
            var models = entities.Select(e => TModel.Create(e));

            return View(models);
        }

        public async Task<ActionResult> Filter(string? type, string? filterText)
        {
            var entities = await _dataAccess.QueryByAsync(type, filterText);
            var models = entities.Select(e => TModel.Create(e));

            ViewBag.FilterText = filterText;
            ViewBag.FilterType = type;
            return View(nameof(Index), models);
        }
        // GET: VehiclesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = default(TModel);
            var vehicle = await _dataAccess.GetByIdAsync(id);

            if (vehicle != default)
            {
                model = TModel.Create(vehicle);
                model.Statistics = await _dataAccess.CalculateStatisticsAsync(id);
                await LoadDrivesAsync(model);
            }
            return model != default ? View(model) : NotFound();
        }

        // GET: VehiclesController/Create
        public async Task<ActionResult> Create()
        {
            var model = TModel.Create(_dataAccess.Create());

            await LoadCompaniesAsync(model);
            return View(model);
        }

        // POST: VehiclesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TModel viewModel)
        {
            try
            {
                var entity = _dataAccess.Create();

                entity.CopyFrom(viewModel);
                await _dataAccess.InsertAsync(entity);
                await _dataAccess.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                await LoadCompaniesAsync(viewModel);
                return View(viewModel);
            }
        }

        // GET: VehiclesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = default(TModel);
            var entity = await _dataAccess.GetByIdAsync(id);

            if (entity != default)
            {
                model = TModel.Create(entity);
                await LoadCompaniesAsync(model);
                await LoadAddDriversAsync(model);
            }
            return View(model);
        }

        // POST: VehiclesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TModel viewModel)
        {
            try
            {
                var entity = await _dataAccess.GetByIdAsync(id);

                if (entity != null)
                {
                    entity.CopyFrom(viewModel);

                    await _dataAccess.UpdateAsync(entity);
                    await _dataAccess.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                await LoadCompaniesAsync(viewModel);
                await LoadAddDriversAsync(viewModel);

                return View(viewModel);
            }
        }

        // GET: VehiclesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehiclesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AddDriver(int vehicleId, int driverId)
        {
            using var driversAccess = HttpContext.RequestServices.GetService<Logic.Contracts.Base.IDriversAccess>();
            var vehicle = await _dataAccess.GetByIdAsync(vehicleId);
            var driver = await driversAccess!.GetByIdAsync(driverId);

            if (vehicle != default && driver != default)
            {
                vehicle.Drivers.Add(driver);

                await _dataAccess.UpdateAsync(vehicle);
                await _dataAccess.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { id = vehicleId });
        }
        public async Task<ActionResult> RemoveDriver(int vehicleId, int driverId)
        {
            using var driversAccess = HttpContext.RequestServices.GetService<Logic.Contracts.Base.IDriversAccess>();
            var vehicle = await _dataAccess.GetByIdAsync(vehicleId);
            var driver = await driversAccess!.GetByIdAsync(driverId);

            if (vehicle != default && driver != default)
            {
                vehicle.Drivers.Remove(driver);

                await _dataAccess.UpdateAsync(vehicle);
                await _dataAccess.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { id = vehicleId });
        }

        public async Task<ActionResult> AddDrive(int vehicleId, int driverId)
        {
            var model = new Models.App.Drive()
            {
                Date = DateTime.UtcNow,
            };

            model.VehicleId = vehicleId;
            model.DriverId = driverId;
            await LoadDriveAsync(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDrive(Models.App.Drive viewModel)
        {
            try
            {
                using var drivesAccess = HttpContext.RequestServices.GetService<Logic.Contracts.App.IDrivesAccess>();
                var entity = drivesAccess!.Create();

                entity.CopyFrom(viewModel);

                await drivesAccess.InsertAsync(entity);
                await drivesAccess.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = viewModel.VehicleId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                await LoadDriveAsync(viewModel);
                return View(viewModel);
            }
        }

        private async Task LoadCompaniesAsync(TModel viewModel)
        {
            using var companiesAccess = HttpContext.RequestServices.GetService<Logic.Contracts.Base.ICompaniesAccess>();

            if (companiesAccess != default)
            {
                var companies = await companiesAccess.GetAllAsync();

                viewModel.Companies = companies.Select(e => Models.Base.Company.Create(e)).ToList();
            }
        }
        private async Task LoadAddDriversAsync(TModel viewModel)
        {
            using var driversAccess = HttpContext.RequestServices.GetService<Logic.Contracts.Base.IDriversAccess>();

            if (driversAccess != default)
            {
                var exitsIds = viewModel.Drivers.Select(d => d.Id).ToArray();
                var drivers = await driversAccess.GetAllAsync();
                var addDrivers = drivers.Where(d => exitsIds.Contains(d.Id) == false);

                viewModel.AddDrivers = addDrivers.Select(d => Models.Base.Driver.Create(d)).ToList();
            }
        }
        private async Task LoadDrivesAsync(TModel viewModel)
        {
            using var drivesAccess = HttpContext.RequestServices.GetService<Logic.Contracts.App.IDrivesAccess>();

            if (drivesAccess != default)
            {
                var drives = await drivesAccess.QueryAsync($"VehicleId=={viewModel.Id}");

                viewModel.Drives = drives.Select(e => Models.App.Drive.Create(e)).ToList();
            }
        }
        private async Task LoadDriveAsync(Models.App.Drive viewModel)
        {
            using var driversAccess = HttpContext.RequestServices.GetService<Logic.Contracts.Base.IDriversAccess>();
            var vehicle = await _dataAccess.GetByIdAsync(viewModel.VehicleId);
            var driver = await driversAccess!.GetByIdAsync(viewModel.DriverId);

            if (vehicle != default && driver != default)
            {
                viewModel.Vehicle = Models.Base.Vehicle.Create(vehicle);
                viewModel.Driver = Models.Base.Driver.Create(driver);
            }
        }
    }
}
