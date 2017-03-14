using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using WindowsPhoneApp.ViewModels;

namespace WindowsPhoneApp.ViewModels
{
    public class UserMetaViewModel : ViewModelBase
    {
        #region Properties

        private int userId = 0;
        public int UserId
        {
            get
            { return userId; }

            set
            {
                if (userId == value)
                { return; }

                userId = value;
                RaisePropertyChanged("UserId");
            }
        }

        private int taskId = 0;
        public int TaskId
        {
            get
            { return taskId; }

            set
            {
                if (taskId == value)
                { return; }

                taskId = value;
                RaisePropertyChanged("TaskId");
            }
        }

        #endregion "Properties"

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public UserMetaViewModel()
        {

        }

        private App app = (Application.Current as App);

        /// <summary>
        /// Retrieve the specific user meta.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="taskId">ID of the task.</param>
        /// <returns>Returns all information about the user meta.</returns>
        public UserMetaViewModel GetUserMeta(int userId, int taskId)
        {
            UserMetaViewModel userMeta = new UserMetaViewModel();

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var _userMeta = (db.Table<Models.UserMeta>().Where(u => u.UserId == userId && u.TaskId == taskId)).Single();
                userMeta.UserId = _userMeta.UserId;
                userMeta.taskId = _userMeta.TaskId;
            }
            return userMeta;
        }

        /// <summary>
        /// Adds a user meta to the database.
        /// </summary>
        /// <param name="userMeta">Helds all information to be inserted into database.</param>
        /// <returns>Returns message on success or failure.</returns>
        public string AddUserMeta(UserMetaViewModel userMeta)
        {
            string result = string.Empty;

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                string change = string.Empty;

                try
                {
                    var existingUserMeta = (db.Table<Models.UserMeta>().Where(u => u.UserId == userMeta.UserId && u.TaskId == userMeta.TaskId)).SingleOrDefault();

                    if (existingUserMeta == null)
                    {
                        int success = db.Insert(new Models.UserMeta()
                        {
                            UserId = userMeta.UserId,
                            TaskId = userMeta.TaskId
                        });
                    }
                    result = "Taak is succesvol gekoppeld.";
                }
                catch (Exception ex)
                {
                    result = "Er is een onbekent probleem opgetreden, probeer het later opnieuw.";
                }
            }
            return result;
        }

        /// <summary>
        /// Updates existing user meta.
        /// </summary>
        /// <param name="userMeta">Helds all information to update user.</param>
        /// <returns>Returns message on success or failure.</returns>
        public string UpdateUserMeta(UserMetaViewModel userMeta)
        {
            string result = string.Empty;

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                string change = string.Empty;

                try
                {
                    var existingUserMeta = (db.Table<Models.UserMeta>().Where(u => u.UserId == userMeta.UserId && u.TaskId == userMeta.TaskId)).SingleOrDefault();

                    if (existingUserMeta != null)
                    {
                        UserId = userMeta.UserId;
                        TaskId = userMeta.TaskId;
                        int success = db.Update(existingUserMeta);
                    }
                    result = "User meta is geupdate.";
                }
                catch (Exception ex)
                {
                    result = "Er is een onbekent probleem opgetreden, probeer het later opnieuw.";
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a user meta from the database.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="taskId">ID of the task.</param>
        /// <returns>Returns message on success or failure.</returns>
        public string DeleteUserMeta(int userId, int taskId)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var existingUserMeta = (db.Table<Models.UserMeta>().Where(u => u.UserId == userId && u.TaskId == taskId)).Single();

                if (db.Delete(existingUserMeta) > 0)
                {
                    result = "User meta is succesvol verwijderen.";
                }
                else
                {
                    result = "Kan user meta niet verwijderen.";
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
