using pfcManager.Model;
using pfcManager.Authorization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

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
        static public void GoAuthotizatePanel()
        {

            mainWindow.Content = new AuthorizationPanel(AuthorizationEntryModel.CreateEnterModel());
        }

        static public void LogOut()
        {
            mainWindow.Content = new AuthorizationPanel(AuthorizationEntryModel.CreateLogOutModel());
        }

        /// <summary>
        /// Переход в главное меню приложения
        /// </summary>
        static public void GoMainMenu()
        {
            mainWindow.Content = new MainMenu();
        }

        /// <summary>
        /// Переход в меню со списком возможной еды
        /// </summary>
        static public void GoFoodListMenu()
        {

        }

        /// <summary>
        /// Открытие окна со статистикой за указанный день
        /// </summary>
        /// <param name="day"></param>
        static public void GoCurrentDayStatistick(DateTime day)
        {

        }
    }
}
