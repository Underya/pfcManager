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
        /// Указание главного окна приложений
        /// </summary>
        /// <param name="mainWindow"></param>
        static void SetMainWindow(MainWindow mainWindow)
        {
            PanelManager.mainWindow = mainWindow;
        }

        /// <summary>
        /// Открытие панели для авторизации
        /// </summary>
        static void GoAuthotizatePanel()
        {
            mainWindow.Content = new AuthorizationPanel();
        }
    }
}
