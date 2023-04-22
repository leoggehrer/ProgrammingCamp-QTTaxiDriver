using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QTTaxiDriver.WpfApp.ViewModels
{
    using TModel = Models.Base.Vehicle;
    using TVehicletype = Logic.Modules.Common.VehicleType;

    public class VehicleViewModel : BaseViewModel
    {
        private TModel? model;
        private List<Models.Base.Driver> _addDrivers = new();
        private List<Models.Base.Company> _companies = new();

        private ICommand? _cmdAddDriver;
        private ICommand? _cmdRemoveDriver;

        public TModel Model
        {
            get => model ??= new TModel();
            set
            {
                model = value;
                // OnPropertychanged(...)
                OnPropertyChanged(nameof(AddDrivers));
                OnPropertyChanged(nameof(CompanyId));
                OnPropertyChanged(nameof(ApprovalDate));
                OnPropertyChanged(nameof(Brand));
                OnPropertyChanged(nameof(VehicleModel));
                OnPropertyChanged(nameof(TypeText));
                OnPropertyChanged(nameof(LicensePlate));
                OnPropertyChanged(nameof(Mileage));
                OnPropertyChanged(nameof(Drivers));
            }
        }

        public Models.Base.Driver[] Drivers => Model.Drivers.ToArray();
        public Models.Base.Driver[] AddDrivers => _addDrivers.ToArray();
        public Models.Base.Company[] Companies => _companies.ToArray();

        public ICommand CommandAddDriver => RelayCommand.Create(ref _cmdAddDriver, async p => await AddDriverAsync().ConfigureAwait(false), p => SelectedAddDriver != default);
        public ICommand CommandRemoveDriver => RelayCommand.Create(ref _cmdRemoveDriver, async p => await RemoveDriverAsync().ConfigureAwait(false), p => SelectedRemoveDriver != default);

        private async Task AddDriverAsync()
        {
            try
            {
                using var vehiclesCtrl = new Logic.Controllers.Base.VehiclesController();
                using var driversCtrl = new Logic.Controllers.Base.DriversController();
                var vehicle = await vehiclesCtrl.GetByIdAsync(Model.Id).ConfigureAwait(false);
                var driver = await driversCtrl.GetByIdAsync(SelectedAddDriver!.Id).ConfigureAwait(false);

                if (vehicle != default && driver != default)
                {
                    vehicle.Drivers.Add(driver);

                    vehicle = await vehiclesCtrl.UpdateAsync(vehicle).ConfigureAwait(false);
                    await vehiclesCtrl.SaveChangesAsync();

                    Model = TModel.Create(vehicle);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in {MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
            }
        }
        private async Task RemoveDriverAsync()
        {
            try
            {
                using var vehiclesCtrl = new Logic.Controllers.Base.VehiclesController();
                using var driversCtrl = new Logic.Controllers.Base.DriversController();
                var vehicle = await vehiclesCtrl.GetByIdAsync(Model.Id).ConfigureAwait(false);
                var driver = await driversCtrl.GetByIdAsync(SelectedAddDriver!.Id).ConfigureAwait(false);

                if (vehicle != default && driver != default)
                {
                    vehicle.Drivers.Remove(driver);

                    vehicle = await vehiclesCtrl.UpdateAsync(vehicle).ConfigureAwait(false);
                    await vehiclesCtrl.SaveChangesAsync();

                    Model = TModel.Create(vehicle);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in {MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
            }
        }


        public VehicleViewModel()
        {
            OnPropertyChanged(nameof(Companies));
            OnPropertyChanged(nameof(AddDrivers));
        }
        public int CompanyId
        {
            get { return Model.CompanyId; }
            set { Model.CompanyId = value; }
        }
        public DateTime ApprovalDate
        {
            get { return Model.ApprovalDate; }
            set { Model.ApprovalDate = value; }
        }

        public string Brand
        {
            get { return Model.Brand; }
            set { Model.Brand = value; }
        }
        public string VehicleModel
        {
            get { return Model.Model; }
            set { Model.Model = value; }
        }
        public string[] Types
        {
            get
            {
                return Enum.GetNames<TVehicletype>();
            }
        }
        public string TypeText
        {
            get { return Model.Type.ToString(); }
            set
            {
                Model.Type = Enum.Parse<TVehicletype>(value);
            }
        }
        public string LicensePlate
        {
            get { return Model.LicensePlate; }
            set { Model.LicensePlate = value; }
        }
        public int Mileage
        {
            get { return Model.Mileage; }
            set { Model.Mileage = value; }
        }

        public Models.Base.Driver? SelectedAddDriver
        {
            get;
            set;
        }
        public Models.Base.Driver? SelectedRemoveDriver
        {
            get;
            set;
        }
        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(AddDrivers))
            {
                Task.Run(LoadAddDriversAsync);
            }
            else if (propertyName == nameof(Companies))
            {
                Task.Run(LoadCompaniesAsync);
            }
            else
            {
                base.OnPropertyChanged(propertyName);
            }
        }

        private async Task LoadAddDriversAsync()
        {
            using var driversAccess = new Logic.Controllers.Base.DriversController();

            if (driversAccess != default)
            {
                var exitsIds = Model.Drivers.Select(d => d.Id).ToArray();
                var drivers = await driversAccess.GetAllAsync();
                var addDrivers = drivers.Where(d => exitsIds.Contains(d.Id) == false);

                _addDrivers.Clear();
                _addDrivers.AddRange(addDrivers.Select(d => Models.Base.Driver.Create(d)));

                if (_addDrivers.Any())
                {
                    SelectedAddDriver = _addDrivers.First();
                }
                else
                {
                    SelectedAddDriver = default;
                }

                base.OnPropertyChanged(nameof(AddDrivers));
                OnPropertyChanged(nameof(SelectedAddDriver));
            }
        }
        private async Task LoadCompaniesAsync()
        {
            using var companiesAccess = new Logic.Controllers.Base.CompaniesController();
                var companies = await companiesAccess.GetAllAsync();

            _companies.Clear();
            _companies.AddRange(companies.Select(e => Models.Base.Company.Create(e)));
            base.OnPropertyChanged(nameof(Companies));
        }
    }
}
