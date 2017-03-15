using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using WindowsPhoneApp.ViewModels;

namespace WindowsPhoneApp.ViewModels
{
    public class UserMetasViewModel : ViewModelBase
    {
        private ObservableCollection<UserMetaViewModel> userMetas;
        public ObservableCollection<UserMetaViewModel> UserMetas
        {
            get
            {
                return userMetas;
            }

            set
            {
                userMetas = value;
                RaisePropertyChanged("UserMetas");
            }
        }

        private WindowsPhoneApp.App app = (Application.Current as App);

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public UserMetasViewModel()
        {

        }

        public ObservableCollection<UserMetaViewModel> GetUsers()
        {
            userMetas = new ObservableCollection<UserMetaViewModel>();
            using (var db = new SQLite.SQLiteConnection(app.DB_PATH))
            {
                var query = db.Table<Models.UserMeta>().OrderBy(u => u.UserId);
                foreach (var _userMeta in query)
                {
                    var userMeta = new UserMetaViewModel()
                    {
                        UserId = _userMeta.UserId,
                        TaskId = _userMeta.TaskId
                    };
                    userMetas.Add(userMeta);
                }
            }
            return UserMetas;
        }
    }
}
