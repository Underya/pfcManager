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

namespace pfcManager.FoodList
{
    /// <summary>
    /// Логика взаимодействия для FoodListFrame.xaml
    /// </summary>
    public partial class FoodListFrame : UserControl
    {
        public FoodListFrame()
        {
            InitializeComponent();
            //Указание контекста
            DataContext = new FoodListVM();
        }
        
        /// <summary>
        /// Изменение текста в окне для поиска еды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //При изменении фильтра для поиска еды
            ((FoodListVM)DataContext).SortText = SortTextBox.Text;
        }
    }
}
