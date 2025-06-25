using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Media;
using ReactiveUI;
using TinyTimer.Models;
using MessageBus = TinyTimer.Messenger.MessageBus;
using ReactiveUI;
using System.Reactive;
using TinyTimer.Messenger;


namespace TinyTimer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand StartTimerCommand { get; }
        public ICommand StopTimerCommand { get; }
        
        public TimerStyle CurrentStyle => TimerStyles.CurrentTimerStyle;
        
        public TimerWindow TimerWindow { get; set; }
        private TimerWindow _timerWindow = new TimerWindow();

        public MainWindowViewModel()
        {
            StartTimerCommand = new DelegateCommand(ExecuteStartTimer);
            StopTimerCommand = new DelegateCommand(ExecuteStopTimer);
        }

        private void ExecuteStartTimer()
        {
            MessageBus.SendStartTimer();
        }

        private void ExecuteStopTimer()
        {
            MessageBus.SendStopTimer();
        }
        
        
        
        
        public void CreateTimerWindow()
        {
            _timerWindow.Show();
        }


        public List<FontFamily> AvailableFonts { get; }
            = FontManager.Current.SystemFonts.OrderBy(f => f.Name)
                .ToList();
        
        private FontFamily _selectedFont = FontFamily.Default;
        public FontFamily SelectedFont
        {
            get => _selectedFont;
            set
            {
                _selectedFont = value;
                OnPropertyChanged(nameof(SelectedFont));
                CurrentStyle.FontFamily = value;
            }
        }
    }
}
