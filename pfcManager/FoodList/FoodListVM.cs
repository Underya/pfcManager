using System;
using System.Collections.Generic;
using System.Text;

namespace pfcManager.FoodList
{
    /// <summary>
    /// View-Model - 
    /// </summary>
    class FoodListVM : PropertyChanges
    {
        /// <summary>
        /// Имя, по которому происходит сортировка
        /// </summary>
        string sortText = "test";

        public string SortText
        {
            get
            {
                return sortText;
            }
            set
            {
                sortText = value;
                OnPropertyChanged("SortText");
            }
        }
    }
}
