using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using WindowsPhoneApp.Models;

namespace WindowsPhoneApp.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        #region Properties

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

        private string title = string.Empty;
        public string Title
        {
            get
            { return title; }

            set
            {
                if (title == value)
                { return; }

                title = value;
                RaisePropertyChanged("Title");
            }
        }

        private string description = string.Empty;
        public string Description
        {
            get
            { return description; }

            set
            {
                if (description == value)
                { return; }

                description = value;
                RaisePropertyChanged("Description");
            }
        }

        private string remarks = string.Empty;
        public string Remarks
        {
            get
            { return remarks; }

            set
            {
                if (remarks == value)
                { return; }

                remarks = value;
                RaisePropertyChanged("Remarks");
            }
        }

        private DateTime date = DateTime.MinValue;
        public DateTime Date
        {
            get
            { return date; }

            set
            {
                if (date == value)
                { return; }

                date = value;
                RaisePropertyChanged("Date");
            }
        }

        private TimeSpan duration;
        public TimeSpan Duration
        {
            get
            { return duration; }

            set
            {
                if (duration == value)
                { return; }

                duration = value;
                RaisePropertyChanged("Duration");
            }
        }

        private decimal costs = 0;
        public decimal Costs
        {
            get
            { return costs; }

            set
            {
                if (costs == value)
                { return; }

                costs = value;
                RaisePropertyChanged("Costs");
            }
        }

        #endregion "Properties"

        private App app = (Application.Current as App);
        
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public TaskViewModel()
        {

        }

        /// <summary>
        /// Retrieve all tasks of the user.
        /// </summary>
        /// <returns>Returns all information about the tasks.</returns>
        public List<TaskViewModel> GetTasks()
        {
            List<TaskViewModel> tasks = new List<TaskViewModel>();
            List<UserMetaViewModel> userMetas = new List<UserMetaViewModel>();
            UserMetaViewModel userMetaViewModel = new UserMetaViewModel();
            userMetas = userMetaViewModel.GetUserMetas();

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var query = (from t in db.Table<Task>()
                             join userMeta in userMetas on t.Id equals userMeta.TaskId
                             where userMeta.UserId == app.CURRENT_USER_ID
                             select t).ToList();

                if (query != null)
                {
                    foreach (var _task in query)
                    {
                        TaskViewModel task = new TaskViewModel()
                        {
                            TaskId = _task.Id,
                            Title = _task.Title,
                            Description = _task.Description,
                            Remarks = _task.Remarks,
                            Date = _task.Date,
                            Duration = _task.Duration,
                            Costs = _task.Costs
                        };
                        tasks.Add(task);
                    }
                }
                else
                {
                    tasks = null;
                }
            }
            return tasks;
        }

        /// <summary>
        /// Retrieve the specific task.
        /// </summary>
        /// <param name="taskId">ID of the task to read.</param>
        /// <returns>Returns all information about the task.</returns>
        public List<TaskViewModel> GetTask(int taskId)
        {
            List<TaskViewModel> taskInfo = new List<TaskViewModel>();
            List<UserMetaViewModel> userMetas = new List<UserMetaViewModel>();
            UserMetaViewModel userMetaViewModel = new UserMetaViewModel();
            userMetas = userMetaViewModel.GetUserMetas();

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var query = (from t in db.Table<Task>()
                             join userMeta in userMetas on t.Id equals userMeta.TaskId
                             where t.Id == taskId
                             select new
                             {
                                 TaskId = t.Id,
                                 UserId = userMeta.TaskId,
                                 Title = t.Title,
                                 Description = t.Description,
                                 Remarks = t.Remarks,
                                 Date = t.Date,
                                 Duration = t.Duration,
                                 Costs = t.Costs
                             });

                if (query != null)
                {
                    foreach (var _task in query)
                    {
                        TaskViewModel task = new TaskViewModel()
                        {
                            TaskId = _task.TaskId,
                            UserId = _task.UserId,
                            Title = _task.Title,
                            Description = _task.Description,
                            Remarks = _task.Remarks,
                            Date = _task.Date,
                            Duration = _task.Duration,
                            Costs = _task.Costs
                        };

                        taskInfo.Add(task);
                    }
                }
                else
                {
                    taskInfo = null;
                }
            }
            return taskInfo;
        }

        /// <summary>
        /// Adds a task to the database.
        /// </summary>
        /// <param name="task">Helds all information to be inserted into database.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool AddTask(Task task)
        {
            bool result = false;
            bool resultUserMeta = false;
            List<UserMeta> userMeta = new List<UserMeta>();
            UserMetaViewModel userMetaViewModel = new UserMetaViewModel();

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                try
                {
                    var existingTask = (from t in db.Table<Task>()
                                        where t.Id == task.Id
                                        select t).SingleOrDefault();

                    if (existingTask == null)
                    {
                        int success = db.Insert(task);
                        resultUserMeta = userMetaViewModel.AddUserMeta(new UserMeta() { UserId = app.CURRENT_USER_ID, TaskId = task.Id });
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
        /// Updates existing task.
        /// </summary>
        /// <param name="task">Helds all information to update task.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool UpdateTask(Task task)
        {
            bool result = false;

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                try
                {
                    var existingTask = (from t in db.Table<Task>()
                                        where t.Id == task.Id
                                        select t).SingleOrDefault();

                    if (existingTask != null)
                    {
                        existingTask.Title = task.Title;
                        existingTask.Description = task.Description;
                        existingTask.Remarks = task.Remarks;
                        existingTask.Date = task.Date;
                        existingTask.Duration = task.Duration;
                        existingTask.Costs = task.Costs;
                        int success = db.Update(existingTask);
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
        /// Deletes a task from the database.
        /// </summary>
        /// <param name="Id">ID of the task to delete.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool DeleteTask(int taskId)
        {
            bool result = false;
            bool resultUserMeta = false;
            List<UserMeta> userMeta = new List<UserMeta>();
            UserMetaViewModel userMetaViewModel = new UserMetaViewModel();

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var existingTask = (from t in db.Table<Task>()
                                    where t.Id == taskId
                                    select t).Single();

                if (db.Delete(existingTask) > 0)
                {
                    result = true;
                    resultUserMeta = userMetaViewModel.DeleteUserMeta(existingTask.Id);
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
