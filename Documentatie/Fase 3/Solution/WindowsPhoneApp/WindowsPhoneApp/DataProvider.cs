using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsPhoneApp.Models;

namespace WindowsPhoneApp
{
    class DataProvider : IDisposable
    {
        private bool disposed = false;
        private string _dbName = "task";

        /// <summary>
        /// DataProvider constructor, initialize object properties.
        /// </summary>
        public DataProvider()
        {

        }

        /// <summary>
        /// DataProvider deconstructor, manages object.
        /// </summary>
        ~DataProvider()
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
        /// Get user list.
        /// </summary>
        /// <returns>Returns user list.</returns>
        public List<Users> GetUserList()
        {
            List<Users> userList = new List<Users>();

            try
            {
                using (var db = new SQLite.SQLiteConnection(_dbName))
                {
                    userList = db.Query<Users>("SELECT * FROM users").ToList();
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
        /// <param name="userInfo">Helds all information to be inserted into database.</param>
        /// <returns>Returns the result.</returns>
        public bool AddUser(Users userInfo)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(_dbName))
                {
                    db.RunInTransaction(() =>
                    {
                        db.Insert(userInfo);
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
        /// <param name="userInfo">Helds all information to update user.</param>
        /// <returns>Returns the result.</returns>
        public bool UpdateUser(int id, Users userInfo)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(_dbName))
                {
                    var user = db.Query<Users>("SELECT * FROM users WHERE Id=" + id).FirstOrDefault();

                    if (user != null)
                    {
                        user.Username = userInfo.Username;
                        user.Password = userInfo.Password;
                        user.Email = userInfo.Email;

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
        /// <returns>Returns the result.</returns>
        public bool DeleteUser(int id)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(_dbName))
                {
                    var user = db.Query<Users>("SELECT * from users WHERE Id=" + id).FirstOrDefault();

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
        /// Get tasks list.
        /// </summary>
        /// <returns>Returns tasks list.</returns>
        public List<Tasks> GetTaskList()
        {
            List<Tasks> taskList = new List<Tasks>();

            try
            {
                using (var db = new SQLite.SQLiteConnection(_dbName))
                {
                    taskList = db.Query<Tasks>("SELECT * FROM tasks").ToList();
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
        /// <param name="taskInfo">Helds all information to be inserted into database.</param>
        /// <returns>Returns the result.</returns>
        public bool AddTask(Tasks taskInfo)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(_dbName))
                {
                    db.RunInTransaction(() =>
                    {
                        db.Insert(taskInfo);
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
        /// <param name="taskInfo">Helds all information to update task.</param>
        /// <returns>Returns the result.</returns>
        public bool UpdateTask(int id, int userId, Tasks taskInfo)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(_dbName))
                {
                    var task = db.Query<Tasks>("SELECT * FROM tasks WHERE Id=" + id + "AND user_id=" + userId).FirstOrDefault();

                    if (task != null)
                    {
                        task.Title = taskInfo.Title;
                        task.Date = taskInfo.Date;
                        task.Duration = taskInfo.Duration;
                        task.Description = taskInfo.Description;
                        task.Remarks = taskInfo.Remarks;
                        task.Costs = taskInfo.Costs;

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
        /// <returns>Returns the result.</returns>
        public bool DeleteTask(int id, int userId)
        {
            bool result = false;

            try
            {
                using (var db = new SQLite.SQLiteConnection(_dbName))
                {
                    var user = db.Query<Users>("SELECT * FROM users WHERE Id=" + id + "AND user_id=" + userId).FirstOrDefault();

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
