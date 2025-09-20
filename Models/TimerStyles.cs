using System.Collections.Generic;

namespace TinyTimer.Models;

// Static class for storing available styles and current style
public static class TimerStyles
{
    public static List<TimerStyle> timerStyles;
    public static TimerStyle CurrentTimerStyle { get; set; } = new TimerStyle();
}