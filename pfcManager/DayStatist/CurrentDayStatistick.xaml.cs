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
using pfcManager.FirstPage;

namespace pfcManager.DayStatist
{
    /// <summary>
    /// Логика взаимодействия для CurrentDayStatistick.xaml
    /// </summary>
    public partial class CurrentDayStatistick : UserControl
    {
        public CurrentDayStatistick()
        {
            InitializeComponent();
        }

        public CurrentDayStatistick(DateTime day) 
        {
            InitializeComponent();
            DataContext = new FirstPageVM(day);
        }

        /// <summary>
        /// Обработка нажатия клавиш 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //Получение веса
                float weight = -1;
                string text = InputWeight.Text;

                //Если был введён вес, но фокус остался на нём же, то в модели свойство не обновится
                if (float.TryParse(text, out weight))
                {
                    //Обновление свойства
                    ((FirstPageVM)DataContext).EatingWeight = weight;
                }

                ((FirstPageVM)DataContext).SaveCommand.Execute(null);
            }
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            object new_obj = sender;
        }

        /// <summary>
        /// Поднимающиеся событие, вызваемое при нажатии мышки на кнопку удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            //Выбор элемента, у которого нажата кнопка
            if (sender is ListBoxItem)
                ((ListBoxItem)sender).IsSelected = true;
        }
    }
}
