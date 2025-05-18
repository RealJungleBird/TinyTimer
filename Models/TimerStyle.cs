using System.ComponentModel;
using Avalonia;
using Avalonia.Media;

namespace TinyTimer.Models
{
    public class TimerStyle : INotifyPropertyChanged
    {
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
        public CornerRadius cr
        {
            get => new CornerRadius(_cornerRadius);
        }

        private int _fontSize;
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
        
        private Color _bgColor = Color.FromArgb(80, 255, 255, 255);
        public Color BgColor 
        {
            get => _bgColor;
            set
            {
                _bgColor = value;
                OnPropertyChanged(nameof(BackgroundBrush));
            }
        }
        public IBrush BackgroundBrush
        {
            get => new SolidColorBrush(_bgColor);
        }
        
        private Color _textColor = Colors.White;
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                OnPropertyChanged(nameof(TextBrush));
            }
        }
        public IBrush TextBrush // converting color to brush so it could be used to color a text
        {
            get => new SolidColorBrush(_textColor);
        }
        
        private Color _textGlowColor = Colors.White;
        public Color TextGlowColor
        {
            get => _textGlowColor;
            set
            {
                _textGlowColor = value;
                OnPropertyChanged(nameof(TextGlowColor));
            }
        }
        public IBrush TextGlowColorBrush
        {
            get => new SolidColorBrush(_textGlowColor);
        }

        private double _textGlowBlur = 0;
        public double TextGlowBlur
        {
            get => _textGlowBlur;
            set
            {
                _textGlowBlur = value;
                OnPropertyChanged(nameof(TextGlowBlur));
            }
        }

        
        
        
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