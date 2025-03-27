using Avalonia.Controls;
using TinyTimer.ViewModels;

namespace TinyTimer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainWindowViewModel();
        (this.DataContext as MainWindowViewModel).CreateTimerWindow();
    }
}