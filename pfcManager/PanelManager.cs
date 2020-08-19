using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using pfcManager.Model;

namespace pfcManager
{
    /// <summary>
    /// Переключение между панелями приложения
    /// </summary>
    static class PanelManager
    {
        /// <summary>
        /// Главное окно приложения
        /// </summary>
        static MainWindow mainWindow = null;

        /// <summary>
        /// Получение id текущего пользователя
        /// </summary>
        public static long CurrentUserId { get { return CurrentUser.Id; } }

        /// <summary>
        /// Авторизированный пользователь
        /// </summary>
        public static UserSave currentUser = null;

        /// <summary>
        /// Указание главного окна приложений
        /// </summary>
        /// <param name="mainWindow"></param>
        static public void SetMainWindow(MainWindow mainWindow)
        {
            PanelManager.mainWindow = mainWindow;
        }

        /// <summary>
        /// Авторизированный пользователь
        /// </summary>
        public static UserSave CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }

        /// <summary>
        /// Открытие панели для авторизации
        /// </summary>
        /// <param name="exit">Если пользователь был авторизирован, надо ли выходить</param>
        static public void GoAuthotizatePanel(bool exit = false)
        {
            mainWindow.Content = new AuthorizationPanel();
        }

        /// <summary>
        /// Переход в главное меню приложения
        /// </summary>
        static public void GoMainMenu()
        {
            mainWindow.Content = new MainMenu();
        }
    }
}
