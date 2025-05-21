using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia.Media;
using TinyTimer.Models;


namespace TinyTimer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TimerStyle CurrentStyle => TimerStyles.CurrentTimerStyle;
        
        public TimerWindow TimerWindow { get; set; }
        private TimerWindow _timerWindow = new TimerWindow();
    
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
