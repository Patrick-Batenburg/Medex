using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Entities
{
    class Tasks
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public decimal Duration { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public decimal Costs { get; set; } 
    }
}
