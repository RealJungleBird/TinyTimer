using Avalonia.Controls;

namespace TinyTimer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        TimerWindow timerWindow = new();
        timerWindow.Show();
    }
}