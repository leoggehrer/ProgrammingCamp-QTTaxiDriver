using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTTaxiDriver.WpfApp.ViewModels
{
    using TModel = Models.Base.Vehicle;

    public class VehicleViewModel : BaseViewModel
    {
        private TModel? model;

        public TModel Model 
        { 
            get => model ??= new TModel();
            set
            {
                model = value;
                // OnPropertychanged(...)
            }
        }

    }
}
