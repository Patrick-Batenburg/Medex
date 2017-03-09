using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Database.Models
{
    [Table("tbl_u_meta")]
    class UserMeta
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}
