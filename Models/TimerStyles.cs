using System.Collections.Generic;

namespace TinyTimer.Models;

public static class TimerStyles
{
    public static List<TimerStyle> timerStyles;
    public static TimerStyle CurrentTimerStyle { get; set; } = new TimerStyle();
}