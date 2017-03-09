using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Database.Models
{
    [Table("tbl_users")]
    class Users
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
