namespace Medex.Models
{
    public class UserMeta
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey, SQLite.NotNull]
        public int Id { get; set; }
        [SQLite.NotNull]
        public int UserId { get; set; }
        [SQLite.NotNull]
        public int TaskId { get; set; }
    }
}
