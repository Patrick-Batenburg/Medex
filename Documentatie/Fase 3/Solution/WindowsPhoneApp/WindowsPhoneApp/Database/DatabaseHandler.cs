using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using WindowsPhoneApp.Database.Models;

namespace WindowsPhoneApp.Database
{
    class DatabaseHandler : IDisposable
    {
        private bool disposed = false;

        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (var db = new SQLiteConnection(DB_PATH))
                    {
                        db.CreateTable<Users>();
                        db.CreateTable<UserMeta>();
                        db.CreateTable<Task>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        } 

        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// DataProvider constructor, initialize object properties.
        /// </summary>
        public DatabaseHandler()
        {

        }

        /// <summary>
        /// DataProvider deconstructor, manages object.
        /// </summary>
        ~DatabaseHandler()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes this this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Determine whenever to dispose this object.
        /// </summary>
        /// <param name="disposing">Determine dispose state.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }
                disposed = true;
            }
        }

        #region User
        /// <summary>
        /// Retrieve the specific user.
        /// </summary>
        /// <param name="id">Id of the user to read.</param>
        /// <returns>Returns all information about the user.</returns>
        public Users ReadUser(int id)
        {
            Users user = new Users();

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    user = db.Query<Users>("SELECT * FROM tbl_users WHERE Id=" + id).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception ex)
            {

            }

            return user;
        }

        /// <summary>
        /// Retrieve the all user list from the database.
        /// </summary>
        /// <returns>Returns user list.</returns>
        public List<Users> ReadUsers()
        {
            List<Users> userList = new List<Users>();

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    userList = db.Query<Users>("SELECT * FROM tbl_users").ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return userList;
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="info">Helds all information to be inserted into database.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool AddUser(Users info)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    db.RunInTransaction(() =>
                    {
                        db.Insert(info);
                    });
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Updates existing user.
        /// </summary>
        /// <param name="id">Id of the user to update.</param>
        /// <param name="info">Helds all information to update user.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool UpdateUser(int id, Users info)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    var user = db.Query<Users>("SELECT * FROM tbl_users WHERE Id=" + id).FirstOrDefault();

                    if (user != null)
                    {
                        user.Username = info.Username;
                        user.Password = info.Password;
                        user.Email = info.Email;

                        db.RunInTransaction(() =>
                        {
                            db.Update(user);
                        });
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="id">Id of the user to delete.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool DeleteUser(int id)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    var user = db.Query<Users>("SELECT * from tbl_users WHERE Id=" + id).FirstOrDefault();

                    if (user != null)
                    {
                        db.RunInTransaction(() =>
                        {
                            db.Delete(user);
                        });
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region Task
        /// <summary>
        /// Retrieve the specific task.
        /// </summary>
        /// <param name="id">Id of the task to read.</param>
        /// <returns>Returns all information about the task.</returns>
        public Tasks ReadTask(int id)
        {
            Tasks task = new Tasks();

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    task = db.Query<Tasks>("SELECT * FROM tbl_users WHERE Id=" + id).FirstOrDefault();
                    return task;
                }
            }
            catch (Exception ex)
            {

            }

            return task;
        }

        /// <summary>
        /// Retrieve the all task list from the database.
        /// </summary>
        /// <returns>Returns tasks list.</returns>
        public List<Tasks> ReadTasks()
        {
            List<Tasks> taskList = new List<Tasks>();

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    taskList = db.Query<Tasks>("SELECT * FROM tbl_tasks").ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return taskList;
        }

        /// <summary>
        /// Adds a task to the database.
        /// </summary>
        /// <param name="info">Helds all information to be inserted into database.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool AddTask(Tasks info)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    db.RunInTransaction(() =>
                    {
                        db.Insert(info);
                    });
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="id">Id of the task to update.</param>
        /// <param name="userId">Id of the user.</param>
        /// <param name="info">Helds all information to update task.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool UpdateTask(int id, int userId, Tasks info)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    var task = db.Query<Tasks>("SELECT * FROM tbl_tasks WHERE Id=" + id + "AND user_id=" + userId).FirstOrDefault();

                    if (task != null)
                    {
                        task.Title = info.Title;
                        task.Date = info.Date;
                        task.Duration = info.Duration;
                        task.Description = info.Description;
                        task.Remarks = info.Remarks;
                        task.Costs = info.Costs;

                        db.RunInTransaction(() =>
                        {
                            db.Update(task);
                        });
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Deletes a task from the database.
        /// </summary>
        /// <param name="id">Id of the task to delete.</param>
        /// <param name="userId">Id of the user.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool DeleteTask(int id, int userId)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    var user = db.Query<Users>("SELECT * FROM tbl_tasks WHERE Id=" + id + "AND user_id=" + userId).FirstOrDefault();

                    if (user != null)
                    {
                        db.RunInTransaction(() =>
                        {
                            db.Delete(user);
                        });
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region UserMeta
        /// <summary>
        /// Retrieve the all user meta list from the database.
        /// </summary>
        /// <returns>Returns user meta list.</returns>
        public List<UserMeta> ReadUserMetas()
        {
            List<UserMeta> list = new List<UserMeta>();

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    list = db.Query<UserMeta>("SELECT * FROM tbl_u_meta").ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return list;
        }

        /// <summary>
        /// Adds user meta to the database.
        /// </summary>
        /// <param name="info">Helds all information to be inserted into database.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool AddUserMeta(Tasks info)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    db.RunInTransaction(() =>
                    {
                        db.Insert(info);
                    });
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Updates existing user meta.
        /// </summary>
        /// <param name="userId">Id of the user to update.</param>
        /// <param name="taskId">Id of the task.</param>
        /// <param name="taskInfo">Helds all information to update user meta.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool UpdateUserMeta(int userId, int taskId, UserMeta info)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    var userMeta = db.Query<UserMeta>("SELECT * FROM tbl_u_meta WHERE user_id=" + userId + "AND task_id=" + taskId).FirstOrDefault();

                    if (userMeta != null)
                    {
                        userMeta.UserId = info.UserId;
                        userMeta.TaskId = info.TaskId;

                        db.RunInTransaction(() =>
                        {
                            db.Update(userMeta);
                        });
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Deletes a task from the database.
        /// </summary>
        /// <param name="id">Id of the user.</param>
        /// <param name="userId">Id of the task.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool DeleteUserMeta(int userId, int taskId)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    var user = db.Query<Users>("SELECT * FROM tbl_u_meta WHERE user_id=" + userId + "AND task_id=" + taskId).FirstOrDefault();

                    if (user != null)
                    {
                        db.RunInTransaction(() =>
                        {
                            db.Delete(user);
                        });
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion
    }
}
