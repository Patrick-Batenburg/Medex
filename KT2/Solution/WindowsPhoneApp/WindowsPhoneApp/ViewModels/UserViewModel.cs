﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Medex.Models;

namespace Medex.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private int id = 0;
        private string username = string.Empty;
        private string password = string.Empty;
        private string email = string.Empty;
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
        public UserViewModel GetUser(int id)
        {
            UserViewModel user = new UserViewModel();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var _user = (from u in db.Table<User>()
                             where u.Id == id
                             select u).Single();

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
        public UserViewModel GetUser(string username, string password)
        {
            UserViewModel user = new UserViewModel();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var _user = (from u in db.Table<User>()
                             where u.Username == username && u.Password == password
                             select u).SingleOrDefault();    

                if (_user != null)
                {
                    user = new UserViewModel()
                    {
                        Id = _user.Id,
                        Username = _user.Username,
                        Password = _user.Password,
                        Email = _user.Email
                    };
                }
                else
                {
                    user = null;
                }
            }
            return user;
        }

        /// <summary>
        /// Retrieve all users.
        /// </summary>
        /// <returns>Returns all information about the users.</returns>
        public ObservableCollection<UserViewModel> GetUsers()
        {
            ObservableCollection<UserViewModel> users = new ObservableCollection<UserViewModel>();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var query = (from u in db.Table<User>()
                             select u).ToList();

                if (query != null)
                {
                    foreach (var _user in query)
                    {
                        UserViewModel user = new UserViewModel()
                        {
                            Id = _user.Id,
                            Username = _user.Username,
                            Password = _user.Password,
                            Email = _user.Email
                        };
                        users.Add(user);
                    }
                }
                else
                {
                    users = null;
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
                    var existingUser = (from u in db.Table<User>()
                                        where u.Id == user.Id
                                        select u).SingleOrDefault();

                    if (existingUser == null)
                    {
                        int success = db.Insert(user);
                    }
                    result = true;
                }
                catch
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
                    var existingUser = (from u in db.Table<User>()
                                        where u.Id == user.Id
                                        select u).SingleOrDefault();

                    if (existingUser != null)
                    {
                        existingUser.Username = user.Username;
                        existingUser.Password = user.Password;
                        existingUser.Email = user.Email;
                        int success = db.Update(existingUser);
                    }
                    result = true;
                }
                catch 
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
                var existingUser = (from u in db.Table<User>()
                                    where u.Id == id
                                    select u).Single();

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

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                if (id == value)
                {
                    return;
                }

                id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                if (username == value)
                {
                    return;
                }

                username = value;
                RaisePropertyChanged("Username");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                if (password == value)
                {
                    return;
                }

                password = value;
                RaisePropertyChanged("Password");
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                if (email == value)
                {
                    return;
                }

                email = value;
                RaisePropertyChanged("Email");
            }
        }
    }
}
