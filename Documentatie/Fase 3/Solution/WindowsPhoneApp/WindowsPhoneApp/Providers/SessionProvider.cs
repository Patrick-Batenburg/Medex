using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using WindowsPhoneApp.Pages;
using Windows.UI.Xaml.Controls;

namespace WindowsPhoneApp
{
    class SessionProvider
    {
        private DispatcherTimer timer;
        private int totalHours;
        private long totalTime;
        private long timeLeft;
        private bool logOff;
        TimeSpan ts;

        /// <summary>
        /// SessionManager constructor.
        /// </summary>
        public SessionProvider()
        {
            timer = new DispatcherTimer();
            ts = TimeSpan.FromHours(4);
            totalTime = ts.Ticks;
            logOff = false;
        }

        /// <summary>
        /// SessionManager custom constructor.
        /// </summary>
        /// <param name="hours">Number of hours.</param>
        public SessionProvider(int hours)
        {
            timer = new DispatcherTimer();
            totalHours = hours;
            ts = TimeSpan.FromHours(totalHours);
            totalTime = ts.Ticks;
            logOff = false;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            if (timer.IsEnabled == true)
            {
                timer.Stop();
            }

            timeLeft = totalTime;
            timer.Tick += TimerTick;
            timer.Start();
        }

        /// <summary>
        /// EventHandler for timer ticks.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event object.</param>
        private void TimerTick(object sender, object e)
        {
            timeLeft--;

            if (timeLeft <= 0)
            {
                timer.Stop();
                logOff = true;
            }
            else
            {
                logOff = false;
            }
        }

        /// <summary>
        /// Checks for session end.
        /// </summary>
        /// <returns>Returns true when session is ended or false when it's not.</returns>
        public bool HasSessionEnded()
        {
            if (logOff == true)
            {
                return logOff;
            }
            else
            {
                return logOff;
            }
        }
    }
}
