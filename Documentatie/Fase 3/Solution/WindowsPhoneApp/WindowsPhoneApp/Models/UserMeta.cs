using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Models
{
    public class UserMeta
    {
        [SQLite.NotNull]
        public int UserId { get; set; }
        [SQLite.NotNull]
        public int TaskId { get; set; }
    }
}
