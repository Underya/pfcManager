using pfcManager.FirstPage;
using pfcManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        string sortText = "";

        /// <summary>
        /// Текст, в соотвествии с которым исчутся блюда
        /// </summary>
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
                OnPropertyChanged("Foods");
            }
        }

        public ObservableCollection<FoodUpd> Foods
        {
            get
            {
                //Создание новой коллекции
                ObservableCollection<FoodUpd> eatingUpdates = new ObservableCollection<FoodUpd>();

                //Получение всей еды, подходящей под описание
                using(ModelContext mc = new ModelContext())
                {
                    List<Food> foods = mc.Food.Where(obj => obj.Name.ToLower().Contains(sortText.ToLower())).
                        OrderBy(o => o.Name).ToList();
                    //Перевод в другой массив и обёртка в производный класс
                    foreach(Food food in foods)
                    {
                        eatingUpdates.Add(new FoodUpd(food));
                    }
                }

                return eatingUpdates;
            }
            set
            {

            }
        }
    }
}
