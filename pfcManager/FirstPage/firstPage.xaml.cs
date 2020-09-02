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

namespace pfcManager.FirstPage
{
    /// <summary>
    /// Логика взаимодействия для firstPage.xaml
    /// </summary>
    public partial class firstPage : UserControl
    {
        public firstPage()
        {
            InitializeComponent();
            DataContext = new FirstPageVM();
        }

        /// <summary>
        /// Обработка нажатия клавиш 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ((FirstPageVM)DataContext).SaveCommand.Execute(null);
            }
        }
    }
}
