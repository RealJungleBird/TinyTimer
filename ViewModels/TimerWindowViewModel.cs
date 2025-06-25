using System;
using System.ComponentModel;
using Avalonia.Media;
using Avalonia.Threading;
using TinyTimer.Messenger;
using TinyTimer.Models;

namespace TinyTimer.ViewModels
{
    public class TimerWindowViewModel : ViewModelBase, IDisposable
    {
        public TimerStyle TimerStyle => TimerStyles.CurrentTimerStyle;
        
        private DispatcherTimer _timer = new DispatcherTimer();
        
        private string _timerText = String.Empty;
        public string TimerText
        {
            get => _timerText;
            set
            {
                _timerText = value;
                OnPropertyChanged(nameof(TimerText));
            }
        }

        private byte _hours = 0;
        private byte _minutes = 0;
        private byte _seconds = 0;

        public TimerWindowViewModel()
        {
            MessageBus.StartTimerRequested += StartTimer;
            MessageBus.StopTimerRequested += StopTimer;
        }

        public void InitializeTimer()
        {
            UpdateTimerText();
            
            _timer = new DispatcherTimer();
            _timer.Tick += TimerOnTick;
            _timer.Interval = TimeSpan.FromSeconds(1);
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        private void ResetTimer()
        {
            _seconds = 0;
            _minutes = 0;
            _hours = 0;
            UpdateTimerText();
        }

        private void TimerOnTick(object? sender, EventArgs e)
        {
            _seconds++;
            if (_seconds >= 60)
            {
                _seconds = 0;
                _minutes++;
            }

            if (_minutes >= 60)
            {
                _minutes = 0;
                _hours++;
            }
            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            TimerText = string.Format("{0}:{1}:{2}",
                _hours.ToString("D2"),
                _minutes.ToString("d2"),
                _seconds.ToString("d2"));
        }

        public void Dispose()
        {
            MessageBus.StartTimerRequested -= StartTimer;
            _timer.Stop();
        }
    }
}