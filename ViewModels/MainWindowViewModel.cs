using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Media;
using TinyTimer.Models;
using MessageBus = TinyTimer.Messenger.MessageBus;
using ReactiveUI;
using System.Reactive;
using TinyTimer.Messenger;

namespace TinyTimer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand StartTimerCommand { get; }      // Command to start timer
    public ICommand StopTimerCommand { get; }       // Command to stop timer
    
    public TimerStyle CurrentStyle => TimerStyles.CurrentTimerStyle;    // Reference to the current style
    
    public TimerWindow TimerWindow { get; set; }
    private TimerWindow _timerWindow = new TimerWindow();
    

    public MainWindowViewModel()
    {
        StartTimerCommand = new DelegateCommand(ExecuteStartTimer);
        StopTimerCommand = new DelegateCommand(ExecuteStopTimer);
    }

    private void ExecuteStartTimer() => MessageBus.SendStartTimer();
    private void ExecuteStopTimer() => MessageBus.SendStopTimer();
    
    
    public void CreateTimerWindow()
    {
        _timerWindow.Show();
    }

    // List of available fonts
    public List<FontFamily> AvailableFonts { get; }
        = FontManager.Current.SystemFonts.OrderBy(f => f.Name)
            .ToList();
    
    private FontFamily _selectedFont = FontFamily.Default;
    public FontFamily SelectedFont
    {
        get => _selectedFont;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedFont, value);
            CurrentStyle.FontFamily = value;
        }
    }
}