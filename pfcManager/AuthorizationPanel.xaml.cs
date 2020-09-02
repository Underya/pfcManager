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
        public AuthorizationPanel(bool exit = false)
        {
            InitializeComponent();
            //Устанока View-Model для объекта
            DataContext = new AuthorizationModel(exit);
        }

        /// <summary>
        /// При загрузке ивызывается метод источника данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Передача информации о событии загрузки
            ((AuthorizationModel)(DataContext)).Load();
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
                ((AuthorizationModel)DataContext).AuthorizationCommands.Execute(PasswordBox);
            }
        }
    }
}
