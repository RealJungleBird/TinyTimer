using System.ComponentModel;
using ReactiveUI;

namespace TinyTimer.ViewModels;

// Base class for all ViewModels with property change notifications
public class ViewModelBase : ReactiveObject, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
