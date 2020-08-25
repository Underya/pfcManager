using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using pfcManager.Model;
using System.Windows.Input;
using System.Windows;

namespace pfcManager.FirstPage
{
    /// <summary>
    /// VM для превой страницы с общей информацией
    /// </summary>
    class FirstPageVM :
        INotifyPropertyChanged
    {

        /// <summary>
        /// Коллекция со всеми видами еды
        /// </summary>
        ObservableCollection<Food> foods =null;

        /// <summary>
        /// Метод для получения всех видов еды
        /// </summary>
        public ObservableCollection<Food> Foods
        {
            get
            {
                return foods ??
                    (foods = new ObservableCollection<Food>(Food.GetAllFood()));
            }
            set
            {
                foods = value;
                OnPropertyChanged("Foods");
            }
        }

        /// <summary>
        /// Коллекция инфорации о еде
        /// </summary>
        ObservableCollection<EatingUpdate> eatings = null;

        /// <summary>
        /// Свойство для изменения информации о еде
        /// </summary>
        public ObservableCollection<EatingUpdate> Eatings 
        {
            get
            {
                //Если пользователь уже сохранил информацию, то она выводится
                if (eatings != null)
                    return eatings;

                //Если не было информации, то создаётся пустая коллекция с 7 значениями
                eatings = new ObservableCollection<EatingUpdate>();
                return eatings;
            }
            set
            {
                eatings = value;
            }
        }

        /// <summary>
        /// Получение количества ккалорий за сегодняшний день за всю еду
        /// </summary>
        public string CurrentCall
        {
            get
            {
                //Сумма калорий за день
                double summ = 0;
                //Получение конца и начала дня
                DateTime start = CurrentDate.StartThisDay(), end = CurrentDate.EndThisDay();
                using(ModelContext mc = new ModelContext())
                {
                    //Получение всех приёмов пищи за сегодя для текущего пользователя
                    var auto = mc.Eating.Where(o => o.Datatime > start && o.Datatime < end && o.Idusers == PanelManager.CurrentUserId);
                    foreach(Eating eating in auto)
                    {
                        double weight = eating.Weight;
                        //Загрузка навигационного свойста
                        using (ModelContext mc2 = new ModelContext())
                        {
                            mc2.Add(eating);
                            mc2.Entry(eating).Reference(o => o.IdfoodNavigation).Load();
                        }
                        double kcal = eating.IdfoodNavigation.Kkal;
                        summ += (new Kcal(kcal, weight));
                    }
                }

                //Получение всего съеденного за сегодня
                return summ.ToString();
            }
            set
            {
                ;
            }
        }

        /// <summary>
        /// Получение и указание последней информации о весе
        /// </summary>
        public float CurrentWeight
        {
            get 
            {
                float weight = 70.0F;
                //Получение последнего актуального веса
                using(ModelContext mc = new ModelContext())
                {
                    var res = mc.Weight.Where(o => o.Idusers == PanelManager.CurrentUserId).OrderByDescending(o=>o.Datatime);
                    //Если нет информации 
                    if (res.Count() == 0) return weight;

                    //Получение самого последнего веса
                    weight = res.FirstOrDefault().Value;
                }

                return weight; 
            }
            set 
            {
                //При получении веса необходимо сохранить результат
                Weight weight = new Weight();
                weight.Value = (float)value;
                weight.Datatime = DateTime.Now;
                weight.Idusers = PanelManager.CurrentUserId;
                using(ModelContext mc = new ModelContext())
                {
                    mc.Weight.Add(weight);
                    mc.SaveChanges();
                }
                OnPropertyChanged("CurrentWeight");
            }
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
