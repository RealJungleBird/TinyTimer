using System.ComponentModel;
using Avalonia;
using Avalonia.Media;
using ReactiveUI;

namespace TinyTimer.Models;

// A class for storing a set of timer style parameters
public class TimerStyle : ReactiveObject
{
    private int _cornerRadius;
    public int CornerRadius
    {
        get => _cornerRadius;
        set => this.RaiseAndSetIfChanged(ref _cornerRadius, value, nameof(cr));
    }
    // Read-only property converting radius value to CornerRadius
    public CornerRadius cr => new CornerRadius(_cornerRadius);

    private int _fontSize;
    public int FontSize
    {
        get => _fontSize;
        set => this.RaiseAndSetIfChanged(ref _fontSize, value);
    }

    private FontFamily _fontFamily;
    public FontFamily FontFamily
    {
        get => _fontFamily;
        set => this.RaiseAndSetIfChanged(ref _fontFamily, value);
    }
    
    private Color _bgColor = Color.FromArgb(80, 255, 255, 255);
    public Color BgColor 
    {
        get => _bgColor;
        set => this.RaiseAndSetIfChanged(ref _bgColor, value, nameof(BackgroundBrush));
    }
    // Converting color to brush so it could be used to color a ui element
    public IBrush BackgroundBrush => new SolidColorBrush(_bgColor);

    
    private Color _textColor = Colors.White;
    public Color TextColor
    {
        get => _textColor;
        set => this.RaiseAndSetIfChanged(ref _textColor, value, nameof(TextBrush));
    }
    // Converting color to brush so it could be used to color a text
    public IBrush TextBrush => new SolidColorBrush(_textColor);
    
    private Color _textGlowColor;
    public Color TextGlowColor
    {
        get => _textGlowColor;
        set => this.RaiseAndSetIfChanged(ref _textGlowColor, value);
    }

    private double _textGlowBlur;
    public double TextGlowBlur
    {
        get => _textGlowBlur;
        set => this.RaiseAndSetIfChanged(ref _textGlowBlur, value);
    }
    
    
    public TimerStyle()
    {
        FontSize = 30;
        CornerRadius = 54;
        TextGlowColor = Colors.Transparent;
        TextGlowBlur = 0;
    }
}