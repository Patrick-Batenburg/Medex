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

        private decimal duration = 0;
        public decimal Duration
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
        /// Retrieve all tasks.
        /// </summary>
        /// <param name="id">ID of the user to read the task.</param>
        /// <returns>Returns all information about the tasks.</returns>
        public List<Task> GetTasks(int id)
        {
            List<Task> tasks = new List<Task>();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var query = db.Table<Task>().ToList();

                foreach (var _task in query)
                {
                    Task task = new Task()
                    {
                        Id = _task.Id,
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
            return tasks;
        }

        /// <summary>
        /// Retrieve the specific task.
        /// </summary>
        /// <param name="id">ID of the task to read.</param>
        /// <returns>Returns all information about the task.</returns>
        public Task GetTask(int id)
        {
            Task task = new  Task();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var _task = (db.Table<Task>().Where(t => t.Id == id)).Single();

                if (_task != null)
                {
                    if (_task.Id == id)
                    {
                        task = new Task()
                        {
                            Id = _task.Id,
                            Title = _task.Title,
                            Description = _task.Description,
                            Remarks = _task.Remarks,
                            Date = _task.Date,
                            Duration = _task.Duration,
                            Costs = _task.Costs
                        };
                    }
                }
            }
            return task;
        }

        /// <summary>
        /// Adds a task to the database.
        /// </summary>
        /// <param name="task">Helds all information to be inserted into database.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public bool AddTask(Task task)
        {
            bool result = false;

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                try
                {
                    var existingTask = (db.Table<Task>().Where(u => u.Id == task.Id)).SingleOrDefault();

                    if (existingTask == null)
                    {
                        int success = db.Insert(task);
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
        /// <returns>Returns message on success or failure.</returns>
        public string UpdateTask(TaskViewModel task)
        {
            string result = string.Empty;

            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                try
                {
                    var existingTask = (db.Table<Task>().Where(t => t.Id == task.Id)).SingleOrDefault();

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
                    result = "Success";
                }
                catch (Exception ex)
                {
                    result = "Failure";
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a task from the database.
        /// </summary>
        /// <param name="Id">ID of the task to delete.</param>
        /// <returns>Returns message on success or failure.</returns>
        public string DeleteTask(int id)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var existingTask = (db.Table<Task>().Where(t => t.Id == id)).Single();

                if (db.Delete(existingTask) > 0)
                {
                    result = "Success";
                }
                else
                {
                    result = "Failure";
                }
            }
            return result;
        }
    }
}
