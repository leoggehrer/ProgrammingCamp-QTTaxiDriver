//@CodeCopy
//MdStart
using Avalonia.Controls;
using ReactiveUI;
using System.Runtime.CompilerServices;

namespace QTTaxiDriver.MvvMApp.ViewModels
{
    public class BaseViewModel : ReactiveObject
    {
        public Window? Window { get; set; }
        internal virtual void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            this.RaisePropertyChanged(propertyName);
        }
    }
}
//MdEnd
