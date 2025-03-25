using Avalonia.Media;

namespace TinyTimer.Models
{
    public class TimerStyle
    {
        public string Greeting { get; } = "Welcome to Avalonia!";
        public  int BgOpacity { get; set; } = 40;
        public  int FontSize { get; set; } = 72;
        public  Color BgColor { get; set; } = Color.FromRgb(255, 255, 255);
        public  Color TextColor { get; set; } = Color.FromRgb(255, 255, 255);
        public  Color TextGlowColor { get; set; } = Color.FromRgb(255, 255, 255);
        public  int TextGlowStrength { get; set; } = 0;
    }
}