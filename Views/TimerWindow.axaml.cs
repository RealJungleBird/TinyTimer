using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using TinyTimer.Models;
using TinyTimer.ViewModels;

namespace TinyTimer;

public partial class TimerWindow : Window
{
    public TimerWindow()
    {
        InitializeComponent();
        this.DataContext = new TimerWindowViewModel();
        // (this.DataContext as TimerWindowViewModel).timerStyle = timerStyle;
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }
}