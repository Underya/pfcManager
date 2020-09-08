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

namespace pfcManager.DayStatist
{
    /// <summary>
    /// Логика взаимодействия для DayStateFrame.xaml
    /// </summary>
    public partial class DayStateFrame : UserControl
    {
        public DayStateFrame()
        {
            InitializeComponent();
            DataContext = new StateFrameMV();
        }

        /// <summary>
        /// Обработка нажатия на строку каллорий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SumKkal sumKkal = ((DockPanel)sender).DataContext as SumKkal;
            ((StateFrameMV)DataContext).OpenStatDay.Execute(sumKkal.Day);
        }
    }
}
