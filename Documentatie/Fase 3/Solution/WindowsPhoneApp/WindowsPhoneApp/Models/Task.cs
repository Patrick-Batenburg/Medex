using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Models
{
    public class Task
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int Id { get; set; }
        [SQLite.NotNull]
        public string Title { get; set; }
        [SQLite.NotNull]
        public DateTime Date { get; set; }
        [SQLite.NotNull]
        public decimal Duration { get; set; }
        [SQLite.NotNull]
        public string Description { get; set; }
        [SQLite.NotNull]
        public string Remarks { get; set; }
        public decimal Costs { get; set; } 
    }
}
