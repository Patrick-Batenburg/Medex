using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using WindowsPhoneApp.Models;

namespace WindowsPhoneApp.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        #region Properties

        private int id = 0;
        public int Id
        {
            get
            { return id; }

            set
            {
                if (id == value)
                { return; }

                id = value;
                RaisePropertyChanged("Id");
            }
        }

        private string username = string.Empty;
        public string Username
        {
            get
            { return username; }

            set
            {
                if (username == value)
                { return; }

                username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string password = string.Empty;
        public string Password
        {
            get
            { return password; }

            set
            {
                if (password == value)
                { return; }

                password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string email = string.Empty;
        public string Email
        {
            get
            { return email; }

            set
            {
                if (email == value)
                { return; }

                email = value;
                RaisePropertyChanged("Email");
            }
        }

        #endregion "Properties"

        private App app = (Application.Current as App);

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public UserViewModel()
        {  

        }

        /// <summary>
        /// Retrieve the specific user.
        /// </summary>
        /// <param name="id">ID of the user to read.</param>
        /// <returns>Returns all information about the user.</returns>
        public User GetUser(int id)
        {
            User user = new User();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                User _user = (db.Table<User>().Where(u => u.Id == id)).Single();
                user.Id = _user.Id;
                user.Username = _user.Username;
                user.Password = _user.Password;
                user.Email = _user.Email;
            }
            return user;
        }

        /// <summary>
        /// Retrieve the specific user.
        /// </summary>
        /// <param name="username">Username of the user to read.</param>
        /// <param name="password">Password of the user to read.</param>
        /// <returns>Returns all information about the user.</returns>
        public User GetUser(string username, string password)
        {
            User user = new User();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var _user = db.Table<User>().Where(u => u.Username == username).Single();

                if (_user != null)
                {
                    if (_user.Password == password)
                    {
                        user = new User()
                        {
                            Id = _user.Id,
                            Username = _user.Username,
                            Password = _user.Password,
                            Email = _user.Email
                        };
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Retrieve all users.
        /// </summary>
        /// <returns>Returns all information about the users.</returns>
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var query = db.Table<User>().ToList();

                foreach (var _user in query)
                {
                    User user = new User()
                    {
                        Id = _user.Id,
                        Username = _user.Username,
                        Password = _user.Password,
                        Email = _user.Email
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user">Helds all information to be inserted into database.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool AddUser(User user)
        {
            bool result = false;

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                try
                {
                    var existingUser = (db.Table<Models.User>().Where(u => u.Id == user.Id)).SingleOrDefault();

                    if (existingUser == null)
                    {
                        int success = db.Insert(user);
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Updates existing user.
        /// </summary>
        /// <param name="user">Helds all information to update user.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool UpdateUser(User user)
        {
            bool result = false;

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                try
                {
                    var existingUser = (db.Table<User>().Where(u => u.Id == user.Id)).SingleOrDefault();

                    if (existingUser != null)
                    {
                        existingUser.Username = user.Username;
                        existingUser.Password = user.Password;
                        existingUser.Email = user.Email;
                        int success = db.Update(existingUser);
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="Id">ID of the user to delete.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool DeleteUser(int id)
        {
            bool result = false;
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var existingUser = (db.Table<User>().Where(u => u.Id == id)).Single();

                if (db.Delete(existingUser) > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private void Login()
        {

        }

        private void Logout()
        {

        }
    }
}
