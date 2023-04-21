using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QTTaxiDriver.AspMvc.Controllers.Base
{
    using TModel = QTTaxiDriver.AspMvc.Models.Base.Vehicle;

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

        // GET: VehiclesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehiclesController/Create
        public ActionResult Create()
        {
            var entity = _dataAccess.Create();

            return View(TModel.Create(entity));
        }

        // POST: VehiclesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: VehiclesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehiclesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
    }
}
