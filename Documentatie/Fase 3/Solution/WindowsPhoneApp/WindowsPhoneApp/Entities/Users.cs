using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Entities
{
    class Users
    {
        private int Id;
        private string Username;
        private string Password;
        private string Email;
        private SessionManager SessionManager;

        public Users()
        {
            SessionManager = new SessionManager();
        }

        private void Login()
        {
            SessionManager.HasSessionEnded();
        }

        private void Logout()
        {
            SessionManager.StartTimer();
        }

        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
