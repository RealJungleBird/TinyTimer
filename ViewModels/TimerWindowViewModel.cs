using Avalonia.Media;
using TinyTimer.Models;

namespace TinyTimer.ViewModels
{
    public class TimerWindowViewModel : ViewModelBase
    {
        public TimerStyle timerStyle => TimerStyles.CurrentTimerStyle;
    }
}