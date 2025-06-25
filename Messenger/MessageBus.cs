using System;

namespace TinyTimer.Messenger;

public static class MessageBus
{
    public static event Action? StartTimerRequested;
    public static event Action? StopTimerRequested;

    public static void SendStartTimer()
    {
        StartTimerRequested?.Invoke();
    }

    public static void SendStopTimer()
    {
        StopTimerRequested?.Invoke();
    }
}