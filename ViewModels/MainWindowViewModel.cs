using Avalonia.Media;
using TinyTimer.Models;

namespace TinyTimer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TimerStyle currentStyle { get; set; } = TimerStyles.CurrentTimerStyle;
        public  int BgOpacity { get; set; } = 40;
        public  int FontSize { get; set; } = 72;
        public  Color BgColor { get; set; } = Color.FromRgb(255, 255, 255);
        public  Color TextColor { get; set; } = Color.FromRgb(255, 255, 255);
        public  Color TextGlowColor { get; set; } = Color.FromRgb(255, 255, 255);
        public  int TextGlowStrength { get; set; } = 0;
    
        public void CreateTimerWindow()
        {
            TimerWindow timerWindow = new TimerWindow();
            timerWindow.Show();
        }
    }
}
