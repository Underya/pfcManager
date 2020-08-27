using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using pfcManager.Model;
using pfcManager.FirstPage;
using pfcManager.DayStatist;
using pfcManager.FoodList;

namespace pfcManager
{
    /// <summary>
    /// Класс предоставляет методы для обновления своих свойств 
    /// </summary>
    class PropertyChanges : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие, вызываемое пр иобновлении
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод, через который происходит указание, что элемнет изменил значение
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
