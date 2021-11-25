using System;
using System.Collections.Generic;
using System.Text;

namespace pfcManager.Model
{
    internal class SavedUserInfo
    {

        public string GetSavedLogin()
        {
            return Settings1.Default.UserLogin;
        }
        public void SaveLogin(string Login)
        {
            Settings1.Default.UserLogin = Login;
            Settings1.Default.Save();
        }

        public string GetSavedPassword()
        {
            return Settings1.Default.PasswordUser;
        }
        public void SavePassword(string Password)
        {
            Settings1.Default.PasswordUser = Password;
            Settings1.Default.Save();
        }

        public bool LoginAndPasswordHasValue()
        {
            if (
                string.IsNullOrEmpty(GetSavedLogin())
                || string.IsNullOrEmpty(GetSavedPassword())
                )
                return false;

            return true;
        }
        public void ClearLoginPassword()
        {
            Settings1.Default.UserLogin = string.Empty;
            Settings1.Default.PasswordUser = string.Empty;
            Settings1.Default.Save();
        }
    }
}
