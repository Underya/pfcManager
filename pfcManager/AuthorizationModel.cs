using pfcManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace pfcManager
{

    /// <summary>
    /// View-Model для панели авторизации
    /// </summary>
    class AuthorizationModel : INotifyPropertyChanged
    {
        AuthorizationCommand authorizationCommand = null;

        public string ErrorText { get; set; }

        public string Login { set; get; }

        public AuthorizationCommand AuthorizationCommand
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
            PasswordBox passwordBox = obj as PasswordBox;
            if (Login == "" || passwordBox.Password == "" || !UserSave.CheckUser(Login, passwordBox.Password))
            {
                ErrorText = "Не удалось авторизироваться с указанным логиным и паролем";
                OnPropertyChanged("ErrorText");
                return;
            }

            MessageBox.Show("ОК!");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
