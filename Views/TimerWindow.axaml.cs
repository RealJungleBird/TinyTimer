using Avalonia.Controls;
using Avalonia.Input;
using TinyTimer.ViewModels;

namespace TinyTimer;

public partial class TimerWindow : Window
{
    public TimerWindow()
    {
        InitializeComponent();
        var viewmodel = new TimerWindowViewModel();
        this.DataContext = viewmodel;
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }
}