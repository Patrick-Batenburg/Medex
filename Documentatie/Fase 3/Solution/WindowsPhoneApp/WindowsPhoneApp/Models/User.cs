namespace Medex.Models
{
    public class User
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey, SQLite.NotNull]
        public int Id { get; set; }
        [SQLite.Unique, SQLite.NotNull]
        public string Username { get; set; }
        [SQLite.NotNull]
        public string Password { get; set; }
        [SQLite.NotNull]
        public string Email { get; set; }
    }
}
