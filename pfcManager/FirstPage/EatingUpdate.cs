using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using pfcManager.Model;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace pfcManager.FirstPage
{
    /// <summary>
    /// Класс еды для обновления 
    /// </summary>
    class EatingUpdate :
        Eating, INotifyPropertyChanged
    {
        public EatingUpdate()
        {
            Id = -1;
        }

        /// <summary>
        /// Получение названия еды
        /// </summary>
        public string FoodName
        {
            get
            {
                //Если еда была загружена
                if (IdfoodNavigation != null)
                    //Отправляется её имя
                    return IdfoodNavigation.Name;

                //Загрузка информации о еде
                FoodLoad();
                //И возращение имени
                return IdfoodNavigation.Name;
            }
            set
            {

            }
        }

        /// <summary>
        /// Калории блюда
        /// </summary>
        public float FoodKkal
        {
            get
            {
                if (IdfoodNavigation != null)
                    return IdfoodNavigation.Kkal;

                FoodLoad();
                return IdfoodNavigation.Kkal;
            }
            set
            {

            }
        }

        /// <summary>
        /// Обновляемый вес объекта
        /// </summary>
        public float WeightUpd
        {
            get { return Weight; }
            set
            {
                Weight = value;
                OnPropertyChanged("WeightUpd");
            }
        }

        /// <summary>
        /// Загрузка информации о связанном блюде
        /// </summary>
        void FoodLoad()
        {
            //Если ещё не была загружена
            using (ModelContext mc = new ModelContext())
            {
                mc.Add(this);
                //То происходит загрузка
                mc.Entry(this).Reference(o => o.IdfoodNavigation).Load();
            }
        }

        /// <summary>
        /// Получение общих ккалорий обеда
        /// </summary>
        public float GetResultKkal
        {
            get
            {
                return (float)(new Kcal(FoodKkal, WeightUpd)).Summ;
            }
            set
            {

            }
        }

        /// <summary>
        /// Обновляемый IDшник
        /// </summary>
        public long IdUpd
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
                OnPropertyChanged("IdUdp");
            }
        }

        /// <summary>
        /// Конструктор для трансформации из базового класса
        /// </summary>
        /// <param name="eating"></param>
        public EatingUpdate(Eating eating)
        {
            Id = eating.Id;
            Weight = eating.Weight;
            Idfood = eating.Idfood;
            Idusers = eating.Idusers;
            Datatime = eating.Datatime;
        }

        /// <summary>
        /// Событие, вызываемое при обновлении
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
