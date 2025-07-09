using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace GenderHealthcare.UI.Services
{
    public class ReminderService : IReminderService
    {
        private readonly List<Timer> _timers = new();

        public void ScheduleOneTime(DateTime triggerTime, string message)
        {
            var now = DateTime.Now;
            var due = triggerTime - now;
            if (due <= TimeSpan.Zero) return;

            var timer = new Timer(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                    MessageBox.Show(message, "Nhắc nhở", MessageBoxButton.OK, MessageBoxImage.Information));
            }, null, due, Timeout.InfiniteTimeSpan);

            _timers.Add(timer);
        }

        public void ScheduleDaily(TimeSpan timeOfDay, string message)
        {
            var now = DateTime.Now;
            var first = now.Date + timeOfDay;
            if (first <= now) first = first.AddDays(1);
            var due = first - now;

            var timer = new Timer(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                    MessageBox.Show(message, "Nhắc nhở", MessageBoxButton.OK, MessageBoxImage.Information));
            }, null, due, TimeSpan.FromDays(1));

            _timers.Add(timer);
        }

        public void ClearAll()
        {
            foreach (var t in _timers) t.Dispose();
            _timers.Clear();
        }
    }
}