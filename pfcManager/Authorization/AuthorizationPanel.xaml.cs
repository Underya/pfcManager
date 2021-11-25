using pfcManager;
using pfcManager.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pfcManager
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPanel.xaml
    /// </summary>
    public partial class AuthorizationPanel : UserControl
    {
        internal AuthorizationPanel(AuthorizationEntryModel authorizationModel)
        {
            InitializeComponent();
            DataContext = authorizationModel;
        }

        /// <summary>
        /// При загрузке ивызывается метод источника данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Передача информации о событии загрузки
            ((AuthorizationEntryModel)(DataContext)).LoadPanel();
        }

        /// <summary>
        /// Обработка нажития клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            //При нажатии на клавишу Enter - попытка авторизации
            if(e.Key == Key.Enter)
            {
                ((AuthorizationEntryModel)DataContext).AuthorizationCommands.Execute(PasswordBox);
            }
        }
    }
}
