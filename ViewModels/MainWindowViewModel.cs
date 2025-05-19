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
        public TimerStyle currentStyle => TimerStyles.CurrentTimerStyle;
    
        public void CreateTimerWindow()
        {
            TimerWindow timerWindow = new TimerWindow();
            timerWindow.Show();
        }


        public List<FontFamily> AvailableFonts { get; }
            = FontManager.Current.SystemFonts.OrderBy(f => f.Name)
                .ToList();
        
        private FontFamily _selectedFont;
        public FontFamily SelectedFont
        {
            get => _selectedFont;
            set
            {
                _selectedFont = value;
                OnPropertyChanged(nameof(SelectedFont));
                currentStyle.FontFamily = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
