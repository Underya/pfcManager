using pfcManager.FirstPage;
using pfcManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Input;

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

        /// <summary>
        /// Команда добавления нового блюда
        /// </summary>
        AcceptCommand addFoodCommand = null;

        /// <summary>
        /// Комманда для добавления новой еды
        /// </summary>
        public AcceptCommand AddFoodCommand
        {
            get
            {
                return addFoodCommand ??
                    (addFoodCommand = new AcceptCommand(AddNewFood));
            }
        }

        /// <summary>
        /// Добавление нового блюда
        /// </summary>
        /// <param name="obj"></param>
        public void AddNewFood(object obj = null)
         {
            //Проверка значений
            if(FootName == null || FootName == string.Empty)
            {
                MessageBox.Show("Не введено название блюда");
                return;
            }

            if(FootKKal == 0)
            {
                MessageBox.Show("Не введна калорийность блюда");
                return;
            }

            using (ModelContext mc = new ModelContext())
            {
                FoodUpd food = new FoodUpd();
                food.Name = FootName;
                food.Kkal = FootKKal;
                food.Protein = Protein;
                food.Fats = Fats;
                food.Carbohydrates = Carb;
                mc.Food.Add(food);
                mc.SaveChanges();
            }

            OnPropertyChanged("Foods");
            ClearFootParams();
        }

        /// <summary>
        /// Очищение всех введённых значений
        /// </summary>
        public void ClearFootParams()
        {
            FootName = string.Empty;
            FootKKal = 0;
            Protein = 0;
            Fats = 0;
            Carb = 0;
        }

        /// <summary>
        /// Список всей еды,
        /// </summary>
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
