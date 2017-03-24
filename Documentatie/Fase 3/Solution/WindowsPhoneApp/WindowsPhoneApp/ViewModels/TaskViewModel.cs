using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Medex.Models;

namespace Medex.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        private int taskId = 0;
        private int userId = 0;
        private string title = string.Empty;
        private string description = string.Empty;
        private string remarks = string.Empty;
        private string date = DateTime.MinValue.ToString("yyyy-MM-dd");
        private string duration = @"00:00";
        private decimal costs = 0;
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
        public ObservableCollection<TaskViewModel> GetTasks()
        {
            ObservableCollection<TaskViewModel> tasks = new ObservableCollection<TaskViewModel>();
            ObservableCollection<UserMetaViewModel> userMetas = new ObservableCollection<UserMetaViewModel>();
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
                            Date = _task.Date.ToString("yyyy-MM-dd"),
                            Duration = String.Format("{0:D2}:{1:D2}",  _task.Duration.Hours, _task.Duration.Minutes),
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
        public ObservableCollection<TaskViewModel> GetTask(int taskId)
        {
            ObservableCollection<TaskViewModel> taskInfo = new ObservableCollection<TaskViewModel>();
            ObservableCollection<UserMetaViewModel> userMetas = new ObservableCollection<UserMetaViewModel>();
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
                                 Duration = String.Format("{0:D2}:{1:D2}", t.Duration.Hours, t.Duration.Minutes),
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
                            Date = _task.Date.ToString("yyyy-MM-dd"),
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
            ObservableCollection<UserMeta> userMeta = new ObservableCollection<UserMeta>();
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
            ObservableCollection<UserMeta> userMeta = new ObservableCollection<UserMeta>();
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

        public int TaskId
        {
            get
            {
                return taskId;
            }

            set
            {
                if (taskId == value)
                {
                    return;
                }

                taskId = value;
                RaisePropertyChanged("TaskId");
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                if (userId == value)
                {
                    return;
                }

                userId = value;
                RaisePropertyChanged("UserId");
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                if (title == value)
                {
                    return;
                }

                title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (description == value)
                {
                    return;
                }

                description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string Remarks
        {
            get
            {
                return remarks;
            }

            set
            {
                if (remarks == value)
                {
                    return;
                }

                remarks = value;
                RaisePropertyChanged("Remarks");
            }
        }

        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                if (date == value)
                {
                    return;
                }

                date = value;
                RaisePropertyChanged("Date");
            }
        }

        public string Duration
        {
            get
            {
                return duration;
            }

            set
            {
                if (duration == value)
                {
                    return;
                }

                duration = value;
                RaisePropertyChanged("Duration");
            }
        }

        public decimal Costs
        {
            get
            {
                return costs;
            }

            set
            {
                if (costs == value)
                {
                    return;
                }

                costs = value;
                RaisePropertyChanged("Costs");
            }
        }

    }
}
