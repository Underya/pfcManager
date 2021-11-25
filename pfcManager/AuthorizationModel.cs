using pfcManager.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace pfcManager
{

    /// <summary>
    /// View-Model для панели авторизации
    /// </summary>
    internal class AuthorizationModel : INotifyPropertyChanged
    {
        public static AuthorizationModel CreateEnterModel()
        {
            return new AuthorizationModel(false);
        }

        public static AuthorizationModel CreateLogOutModel()
        {
            return new AuthorizationModel(true);
        }

        protected AuthorizationModel(bool exit = false)
        {
            SaveUser = true;
            //Если происходит выход
            if (exit)
            {
                SaveUser = false;
                ClearSetting();
            }
        }

        AuthorizationCommand authorizationCommand = null;

        public string ErrorText { get; set; }
        public string Login { set; get; }
        /// <summary>
        /// Сохранение информации о том, надо ли запоминать пользователя
        /// </summary>
        public bool SaveUser { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        string password = "";

        /// <summary>
        /// Указание, что произошла загрузка окна приложения
        /// </summary>
        public void Load()
        {
            //Попытка получить информацию о логине и пароле
            if (Settings1.Default.UserLogin == string.Empty)
                return;
            if (Settings1.Default.PasswordUser == string.Empty)
                return;

            //Непосредственно авторизация
            Authorization(Settings1.Default.UserLogin, Settings1.Default.PasswordUser);
        }

        public AuthorizationCommand AuthorizationCommands
        {
            get
            {
                return authorizationCommand ??
                    (authorizationCommand = new AuthorizationCommand(ExecuteAuthorizateCommand));
            }
        }

        /// <summary>
        /// Непосредственно выполнение комманды авторизации
        /// </summary>
        /// <param name="obj"></param>
        void ExecuteAuthorizateCommand(object obj)
        {
            //Указание пользователю
            ErrorText = "Авторизация...";
            PasswordBox passwordBox = obj as PasswordBox;
            Authorization(Login, passwordBox.Password);
        }

        /// <summary>
        /// Команда авторизации
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="password"></param>
        void Authorization(string Login, string password)
        {
            if (Login == "" || password == "" || !UserSave.CheckUser(Login, password))
            {
                ErrorText = "Не удалось авторизироваться с указанным логиным и паролем";
                OnPropertyChanged("ErrorText");
                return;
            }

            this.password = password;

            //Если необходимо, то сохраняется логин и пароль
            if (SaveUser)
                SaveLoginPassword();
            //В противном случае - наборот очищаются настройки
            else
                ClearSetting();

            PanelManager.CurrentUser = UserSave.GetUser(Login);
            PanelManager.GoMainMenu();
        }

        /// <summary>
        /// Сохранение лоигна и пароля в настройках, если флаг указан
        /// </summary>
        void SaveLoginPassword()
        {
            //Если произошёл автозаход, то не надо сохранять параметр
            if (Login == string.Empty || password == string.Empty
                || Login == null || password == null)
                return;

            Settings1.Default.UserLogin = Login;
            Settings1.Default.PasswordUser = password;
            Settings1.Default.Save();
        }

        /// <summary>
        /// Очищение сохранений
        /// </summary>
        void ClearSetting()
        {
            Settings1.Default.UserLogin = string.Empty;
            Settings1.Default.PasswordUser = string.Empty;
            Settings1.Default.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
