using System.ComponentModel;
using Avalonia;
using Avalonia.Media;

namespace TinyTimer.Models
{
    public class TimerStyle : INotifyPropertyChanged
    {
        public  int BgOpacity { get; set; } = 40;

        public CornerRadius cr
        {
            get => new CornerRadius(_cornerRadius);
        }
        private int _cornerRadius;
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                if (value != _cornerRadius)
                {
                    _cornerRadius = value;
                    OnPropertyChanged(nameof(cr));
                }
            }
        }

        private int _fontSize = 12;
        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (value != _fontSize)
                {
                    _fontSize = value;
                    OnPropertyChanged(nameof(FontSize));
                }
            }
        }
        
        public  Color BgColor { get; set; } = Color.FromRgb(255, 255, 255);
        
        private Color _textColor = Colors.White;
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                OnPropertyChanged(nameof(TextColor));
            }
        }
        
        
        public  Color TextGlowColor { get; set; } = Color.FromRgb(255, 255, 255);
        public  int TextGlowStrength { get; set; } = 0;

        public TimerStyle()
        {
            FontSize = 30;
            CornerRadius = 54;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}