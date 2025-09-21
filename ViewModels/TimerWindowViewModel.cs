using System;
using System.ComponentModel;
using Avalonia.Media;
using Avalonia.Threading;
using ReactiveUI;
using TinyTimer.Models;
using MessageBus = TinyTimer.Messenger.MessageBus;

namespace TinyTimer.ViewModels
{
    public class TimerWindowViewModel : ViewModelBase, IDisposable
    {
        public TimerStyle TimerStyle => TimerStyles.CurrentTimerStyle;
        
        private DispatcherTimer _timer = new DispatcherTimer();
        
        private string _timerText = string.Empty;
        public string TimerText
        {
            get => _timerText;
            set => this.RaiseAndSetIfChanged(ref _timerText, value);
        }

        private byte _hours = 0;
        public byte Hours
        {
            get => _hours;
            set => this.RaiseAndSetIfChanged(ref _hours, value);
        }
        
        private byte _minutes = 0;
        public byte Minutes
        {
            get => _minutes;
            set => this.RaiseAndSetIfChanged(ref _minutes, value);
        }
        
        private byte _seconds = 0;
        public byte Seconds
        {
            get => _seconds;
            set => this.RaiseAndSetIfChanged(ref _seconds, value);
        }

        private bool _isTimerRunning = false;
        public bool IsTimerRunning
        {
            get => _isTimerRunning;
            set => this.RaiseAndSetIfChanged(ref _isTimerRunning, value);
        }

        public TimerWindowViewModel()
        {
            MessageBus.StartTimerRequested += StartTimer;
            MessageBus.StopTimerRequested += StopTimer;
            
            this.WhenAnyValue(x => x.Hours, x => x.Minutes, x => x.Seconds)
                .Subscribe(_ => UpdateTimerText());
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
            IsTimerRunning = true;
        }

        public void StopTimer()
        {
            _timer.Stop();
            IsTimerRunning = false;
        }

        public void ResetTimer()
        {
            Seconds = 0;
            Minutes = 0;
            Hours = 0;
        }

        private void TimerOnTick(object? sender, EventArgs e)
        {
            Seconds++;
            if (Seconds >= 60)
            {
                Seconds = 0;
                Minutes++;
            }

            if (Minutes >= 60)
            {
                Minutes = 0;
                Hours++;
            }
        }

        private void UpdateTimerText()
        {
            TimerText = $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        }

        public void Dispose()
        {
            MessageBus.StartTimerRequested -= StartTimer;
            MessageBus.StopTimerRequested -= StopTimer;
            _timer.Stop();
        }
    }
}