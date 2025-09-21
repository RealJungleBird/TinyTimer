using Avalonia.Controls;
using Avalonia.Interactivity;
using TinyTimer.ViewModels;

namespace TinyTimer.Views;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel; 
    
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainWindowViewModel();
        this.DataContext = _viewModel;
        _viewModel.CreateTimerWindow();
    }
}