using Avalonia.Controls;
using TinyTimer.ViewModels;

namespace TinyTimer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var viewModel = new MainWindowViewModel();
        this.DataContext = viewModel;
        viewModel.CreateTimerWindow();
    }
}