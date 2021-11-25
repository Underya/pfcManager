using pfcManager.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace pfcManager.Authorization
{
    /// <summary>
    /// View-Model для панели авторизации
    /// </summary>
    internal class AuthorizationEntryModel : 
        INotifyPropertyChanged
    {
        public static AuthorizationEntryModel CreateEnterModel()
        {
            return new AuthorizationEntryModel();
        }
        public static AuthorizationEntryModel CreateLogOutModel()
        {
            return new AuthorizationLogOutModel();
        }
        protected AuthorizationEntryModel()
        {

        }

        public string ErrorText { get; set; }
        public string Login { set; get; }
        /// <summary>
        /// Сохранение информации о том, надо ли запоминать пользователя
        /// </summary>
        public bool SaveUser { get; set; } = true;
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        protected string Password { get; set; } = "";
        AuthorizationCommand authorizationCommand = null;

        /// <summary>
        /// Указание, что произошла загрузка окна приложения
        /// </summary>
        public void LoadPanel()
        {
            SavedUserInfo userInfo = new SavedUserInfo();
            //Попытка получить информацию о логине и пароле
            if (userInfo.LoginAndPasswordHasValue())
            {
                //Непосредственно авторизация
                Login = userInfo.GetSavedLogin();
                Password = userInfo.GetSavedPassword();
                Authorization();
            }
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
            PasswordBox passwordBox = obj as PasswordBox;
            Password = passwordBox.Password;
            Authorization();
        }
         
        protected void Authorization()
        {
            SetErrorText("Авторизация...");

            if(TryLogin())
                SuccessLogin();
        }
        bool TryLogin()
        {
            if (string.IsNullOrEmpty(Login))
            {
                SetErrorText("Не удалось авторизироваться. Не введён логин пользователя");
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                SetErrorText("Не удалось авторизироваться. Не введён пароль пользователя");
                return false;
            }
            if (!UserSave.CheckUser(Login, Password))
            {
                SetErrorText("Не удалось авторизироваться с указанным логиным и паролем");
                return false;
            }

            return true;
        }
        void SetErrorText(string text)
        {
            ErrorText = text;
            OnPropertyChanged("ErrorText");
        }
        void SuccessLogin()
        {
            //Если необходимо, то сохраняется логин и пароль
            if (SaveUser)
                SaveLoginPassword();
            //В противном случае - наборот очищаются настройки
            else
                ClearLoginPassword();

            PanelManager.CurrentUser = UserSave.GetUser(Login);
            PanelManager.GoMainMenu();
        }
        void SaveLoginPassword()
        {
            SavedUserInfo userInfo = new SavedUserInfo();
            userInfo.SaveLogin(Login);
            userInfo.SavePassword(Password);
        }
        protected void ClearLoginPassword()
        {
            SavedUserInfo info = new SavedUserInfo();
            info.ClearLoginPassword();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
