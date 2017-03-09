using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhoneApp.Database;

namespace WindowsPhoneApp.Entities
{
    class Users
    {
        private int id;
        private string username;
        private string password;
        private string email;
        private SessionManager sessionManager;
        private DatabaseHandler dbHandler;

        public Users()
        {
            dbHandler = new DatabaseHandler();
            sessionManager = new SessionManager();
        }

        private void Login()
        {
            sessionManager.HasSessionEnded();
        }

        private void Logout()
        {
            sessionManager.StartTimer();
        }
    }
}
