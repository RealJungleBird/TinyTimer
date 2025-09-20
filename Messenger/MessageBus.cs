using System;

namespace TinyTimer.Messenger;

// Message Bus pattern for loose coupling between components
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