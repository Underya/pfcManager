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

        /// <summary>
        /// Переменная для хранения названия нового блюда
        /// </summary>
        string footName = "";

        /// <summary>
        /// Название нового блюда
        /// </summary>
        public string FootName 
        {
            get
            {
                return footName;
            }
            set
            {
                footName = value;
                OnPropertyChanged(FootName);
            }
        }

        /// <summary>
        /// Калорийность по умолчанию
        /// </summary>
        float footKkal = 0;

        /// <summary>
        /// Ккалорийность нового блюда
        /// </summary>
        public float FootKKal 
        {
            get
            {
                return footKkal;
            }
            set
            {
                footKkal = value;
                OnPropertyChanged("FootKkal");
            }
        }

        /// <summary>
        /// Переменная для хранения значения протеина нового блюда
        /// </summary>
        float protein = 0;

        /// <summary>
        /// Свойство для получения нового блюда
        /// </summary>
        public float Protein
        {
            get
            {
                return protein;
            }
            set
            {
                protein = value;
                OnPropertyChanged("Protein");
            }
        }

        /// <summary>
        /// Переменная для хранения жиров
        /// </summary>
        float fats = 0;

        /// <summary>
        /// Свойство для получения значения жиров
        /// </summary>
        public float Fats
        {
            get 
            {
                return fats;
            }
            set
            {
                fats = value;
                OnPropertyChanged("Fats");
            }
        }

        /// <summary>
        /// Переменная для хранения углеводов
        /// </summary>
        float carb = 0;

        /// <summary>
        /// Свойство для получения значения углеводов
        /// </summary>
        public float Carb
        {
            get 
            { 
                return carb; 
            }
            set
            {
                carb = value;
                OnPropertyChanged("Carb");
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
