using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Medex.Views;

namespace Medex.Providers
{
    public class SessionProvider
    {
        private DateTime activated = DateTime.MinValue;
        private DateTime deactivated = DateTime.MinValue;
        //private TimeSpan result;

        public SessionProvider()
        {
            activated = Activated;
            deactivated = Deactivated;
        }

        public SessionProvider(DateTime activated, DateTime deactivated)
        {
            this.activated = activated;
            this.deactivated = deactivated;
        }

        public DateTime SaveDeactivatedState() 
        {
            bool pageCheck = CheckPage();

            if (pageCheck == false)
            {
                deactivated = DateTime.MinValue;
            }
            else
            {
                deactivated = DateTime.Now;
            }

            return deactivated;
        }

        public DateTime SaveActivatedState()
        {
            bool pageCheck = CheckPage();

            if (pageCheck == false)
            {
                activated = DateTime.MinValue;
            }
            else
            {
                activated = DateTime.Now;
            }

            return activated;
        }

        public int GetInactivity()
        {
            int result = 0;
            result = activated.Subtract(deactivated).Hours;

            if (result > 5 && activated.Date != DateTime.MinValue && deactivated.Date != DateTime.MinValue)
            {
                var frame = (Frame)Window.Current.Content;
                frame.Navigate(typeof(MainPage));
            }

            return result;
        }

        private bool CheckPage()
        {
            bool result = false;
            var page = (Window.Current.Content as Frame).Content.GetType();

            if (page != typeof(MainPage) && page != typeof(RegisterPage) && page != typeof(LoginPage))
            {
                result = true;
            }

            return result;
        }

        public DateTime Activated 
        {
            get
            {
                return activated;
            }

            set
            {
                if (activated == DateTime.Now)
                {
                    return;
                }

                activated = value;
            }
        }

        public DateTime Deactivated 
        {
            get
            {
                return deactivated;
            }

            set
            {
                if (deactivated == DateTime.Now)
                {
                    return;
                }

                deactivated = value;
            }
        }
    }
}
