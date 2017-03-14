using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Models
{
    public class User
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int Id { get; set; }
        [SQLite.Unique]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
