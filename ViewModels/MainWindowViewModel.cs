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
    }
}
