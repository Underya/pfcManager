using System;
using System.Collections.Generic;
using System.Text;
using pfcManager.Model;

namespace pfcManager
{
    /// <summary>
    /// Model-View для главного меню приложения
    /// </summary>
    class MainMenuVM
    {
        /// <summary>
        /// Указание на профиль текушего пользователя 
        /// </summary>
        UserSave userSave = null;

        /// <summary>
        /// Модель для управления панелью
        /// </summary>
        public MainMenuVM()
        {
            userSave = PanelManager.CurrentUser;
        }

        /// <summary>
        /// Создание команды для выхода из профиля
        /// </summary>
        public ExitCommand exitCommand { get { return new ExitCommand(); } }

        /// <summary>
        /// Имя текущего пользователя
        /// </summary>
        public string UserName
        {
            get
            {
                return userSave.Lastname + " " + userSave.Firstname + " " + userSave.Midname;
            }
        }
    }
}
