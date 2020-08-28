using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using pfcManager.Model;

namespace pfcManager.FoodList
{
    /// <summary>
    ///  Модель еды с автобновлением некоторых парамтеров
    /// </summary>
    class FoodUpd : 
        Food, 
        INotifyPropertyChanged
    {
        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        public FoodUpd()
        {

        }

        /// <summary>
        /// Конструктор для трансформации из базового типа в обновляемый
        /// </summary>
        /// <param name="food"></param>
        public FoodUpd(Food food)
        {
            //Сохранение всех параметров
            this.Id = food.Id;
            this.Name = food.Name;
            this.Kkal = food.Kkal;
            this.Description = food.Description;
            this.Carbohydrates = food.Carbohydrates;
            if (Carbohydrates == null)
                Carbohydrates = 0;
            this.Eating = food.Eating;
            this.Fats = food.Fats;
            if (Fats == null)
                Fats = 0;
            this.Protein = food.Protein;
            if(Protein == null)
                Protein = 0;
        }

        /// <summary>
        /// Обновляемые белки
        /// </summary>
        public float? ProteinUpd
        {
            get { return Protein; }
            set
            {
                Protein = value;
                SaveChange();
                OnPropertyChanged("ProteinUpd");
            }
        }

        /// <summary>
        /// Обновляемые жиры
        /// </summary>
        public float? FatsUpd
        {
            get
            {
                return Fats;
            }
            set
            {
                Fats = value;
                SaveChange();
                OnPropertyChanged("FatsUpd");
            }
        }

        /// <summary>
        /// Углеводы
        /// </summary>
        public float? CarbohydratesUpd
        {
            get { return Carbohydrates; }
            set
            {
                Carbohydrates = value;
                SaveChange();
                OnPropertyChanged("CarbohydratesUpd");
            }
        }

        /// <summary>
        /// Обновляемое описание еды
        /// </summary>
        public string DescriptionUpd
        {
            get { return Description; }
            set
            {
                Description = value;
                SaveChange();
                OnPropertyChanged("DescriptionUpd");
            }
        }

        /// <summary>
        /// Обновляемые калории
        /// </summary>
        /// <returns></returns>
        public float KkalUpd
        {
            get { return Kkal; }
            set
            {
                Kkal = value;
                SaveChange();
                OnPropertyChanged("KkalUpd");
            }
        }

        /// <summary>
        /// Обновляемое название еды
        /// </summary>
        public string NameUpd
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
                SaveChange();
                OnPropertyChanged("NameUpd");
            }

        }

        /// <summary>
        /// Сохранение изменений объекта
        /// </summary>
        void SaveChange()
        {
            using(ModelContext mc = new ModelContext())
            {
                mc.Attach(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                mc.SaveChanges();
            }
        }

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
