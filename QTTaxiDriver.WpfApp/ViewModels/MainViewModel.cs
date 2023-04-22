//@CodeCopy
//MdStart
namespace QTTaxiDriver.WpfApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using TVehicle = Models.Base.Vehicle;

    public partial class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            OnPropertyChanged(nameof(Vehicles));
        }

        #region fields
        private ICommand? _cmdAdd;
        private ICommand? _cmdEdit;
        private ICommand? _cmdDelete;

        private List<TVehicle> _vehicles = new();
        private TVehicle? selectedVehicle;
        private string? filterText;
        #endregion fields

        public TVehicle? SelectedVehicle
        {
            get => selectedVehicle;
            set => selectedVehicle = value;
        }
        public string? FilterText 
        { 
            get => filterText;
            set
            {
                filterText = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }

        public ICommand CommandAdd => RelayCommand.Create(ref _cmdAdd, p => AddVehicle());

        private void AddVehicle()
        {
            throw new NotImplementedException();
        }
        public ICommand CommandEdit => RelayCommand.Create(ref _cmdEdit, p => EditVehicle(), p => SelectedVehicle != default);

        private void EditVehicle()
        {
            VehicleWindow window = new VehicleWindow();

            window.ViewModel.Model = SelectedVehicle!;
            window.ShowDialog();
            OnPropertyChanged(nameof(Vehicles));
        }

        public ICommand CommandDelete => RelayCommand.Create(ref _cmdDelete, p => DeleteVehicle(), p => SelectedVehicle != default);

        private void DeleteVehicle()
        {
            throw new NotImplementedException();
        }

        public TVehicle[] Vehicles => _vehicles.ToArray();

        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(Vehicles))
            {
                Task.Run(LoadVehiclesAsync);
            }
            else
            {
                base.OnPropertyChanged(propertyName);
            }
        }

        private async Task LoadVehiclesAsync()
        {
            using var ctrl = new Logic.Controllers.Base.VehiclesController();
            var entities = await ctrl.QueryByAsync(string.Empty, filterText).ConfigureAwait(false);
            var models = entities.Select(e => TVehicle.Create(e));

            _vehicles.Clear();
            _vehicles.AddRange(models);

            base.OnPropertyChanged(nameof(Vehicles));
        }
    }
}
//MdEnd
