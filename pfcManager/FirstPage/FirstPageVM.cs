using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
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
        /// День, о котором получается и сохраняется информация
        /// </summary>
        DateTime CurrentDay = DateTime.Now;

        /// <summary>
        /// Комманда удаления позиции
        /// </summary>
        AcceptCommand deleteEatComm = null;
        
        /// <summary>
        /// Комманда удаления позиция из списка
        /// </summary>
        public AcceptCommand DeleteEatingCommand
        {
            get
            {
                return deleteEatComm ??
                    (deleteEatComm = new AcceptCommand(obj =>
                    {
                        //Если не выбрано окно
                        if (obj == null)
                        {
                            MessageBox.Show("Не выбран приём пищи!");
                            return;
                        }

                        EatingUpdate eatingUpdate = (EatingUpdate)obj;
                        using(ModelContext mc = new ModelContext())
                        {
                            mc.Eating.Remove(eatingUpdate);
                            mc.SaveChanges();
                        }
                        //Обновление каллорий за день
                        OnPropertyChanged("CurrentCall");
                        //Обновление всей еды за сегодня
                        OnPropertyChanged("Eatings");
                    }));
            }
        }

        /// <summary>
        /// Выбрнная еда из мнею добавления
        /// </summary>
        EatingUpdate selectEatingUpd;

        /// <summary>
        /// Конкструктор с созданием объекта для текущего дня
        /// </summary>
        public FirstPageVM() { }

        /// <summary>
        /// Создание модели для указанного дня
        /// </summary>
        /// <param name="CurrentDay"></param>
        public FirstPageVM(DateTime CurrentDay)
        {
            this.CurrentDay = CurrentDay;
        }

        /// <summary>
        /// Выбранная еда в меню еды
        /// </summary>
        public EatingUpdate SelectEatingUpd 
        {
            get
            {
                return selectEatingUpd;
            }
            set
            {
                selectEatingUpd = value;
                OnPropertyChanged("SelectEatingUpd");
            }
        }

        /// <summary>
        /// Комманда добавления нового блюда
        /// </summary>
        public AcceptCommand SaveCommand 
        { 
            get
            {
                return new AcceptCommand(o =>
                {
                    try
                    {
                        //Если была успешно добавлена деа
                        if( AddNewEating() == 1)
                            //Очищается меню ввода
                            ClearDate();
                    } catch(Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Ошибка!");
                        return;
                    }
                });
            } 
        }

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
        ObservableCollection<EatingUpdate> eatings = new ObservableCollection<EatingUpdate>();

        /// <summary>
        /// Текущая выбрнная еда
        /// </summary>
        Food food = null;

        /// <summary>
        /// Выбранная в комбобоксе еда
        /// </summary>
        public Food SelectedFood 
        { 
            get 
            {
                return food ??
                    (food = new Food()); 
            }
            set 
            {
                food = value;
                OnPropertyChanged("SelectedFood");
                OnPropertyChanged("ResultKkal");
            } 
        }

        /// <summary>
        /// Вес блюда, указнный в форме
        /// </summary>
        float eatingWeight = 0.0F;

        /// <summary>
        /// </summary>
        /// Вес блюда
        public float EatingWeight 
        {
            get
            {
                return eatingWeight;
            }
            set
            {
                eatingWeight = value;
                OnPropertyChanged("EatingWeight");
                OnPropertyChanged("ResultKkal");
            }
        }

        /// <summary>
        /// Результирующая калорийность блюда
        /// </summary>
        public double ResultKkal
        {
            get
            {
                return (new Kcal(SelectedFood.Kkal, EatingWeight));
            }
            set
            {

            }
        }

        /// <summary>
        /// Свойство для изменения информации о еде
        /// </summary>
        public ObservableCollection<EatingUpdate> Eatings 
        {
            get
            {
                //Очищение
                eatings.Clear();
                //Получение всей информации о еде за сегодня
                List<Eating> eatingsCol = Eating.GetEatings(CurrentDay);
                //Обёртка их в класс коллекции
                foreach(Eating eating in eatingsCol)
                {
                    eatings.Add(new EatingUpdate(eating));
                }

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
                DateTime start = CurrentDate.StartCurrentDay(CurrentDay), end = CurrentDate.EndCurrentDay(CurrentDay);
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
                return Math.Round(summ, 1).ToString();
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
                //ПОлучение информации о дне
                DateTime start = CurrentDate.StartCurrentDay(CurrentDay), end = CurrentDate.EndCurrentDay(CurrentDay);
                //Получение последнего актуального веса
                using(ModelContext mc = new ModelContext())
                {
                    var res = mc.Weight.
                        //Только для текущего пользователя
                        Where(o => o.Idusers == PanelManager.CurrentUserId).
                        //Только в указанный день
                        Where(o => o.Datatime >= start && o.Datatime <= end).
                        //Получение последней записи
                        OrderByDescending(o=>o.Datatime);

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
                //Также, при имзенении веса, может изменится рекомендуемое количество ккал
                OnPropertyChanged("RecommendKcal");
            }
        }

        /// <summary>
        /// Очищение информации 
        /// </summary>
        void ClearDate()
        {
            EatingWeight = 0;
            SelectedFood = null;
            OnPropertyChanged("EatingWeight");
            OnPropertyChanged("SelectedFood");
            OnPropertyChanged("Foods");
        }

        /// <summary>
        /// Добавление нового блюда
        /// </summary>
        int AddNewEating()
        {
            if(SelectedFood == null || SelectedFood.Id == 0)
            {
                MessageBox.Show("Не выбрано блюдо");
                return 0;
            }
                
            if(EatingWeight == 0.0) 
            {
                MessageBox.Show("Не указан вес блюда");
                return 0;
            }

            Eating eating = new Eating();

            //Указание даты добавления еды
            DateTime now = DateTime.Now;
            eating.Datatime = new DateTime(CurrentDay.Year, CurrentDay.Month, CurrentDay.Day, now.Hour, now.Minute, now.Second);

            eating.Idfood = SelectedFood.Id;
            eating.Idusers = PanelManager.CurrentUserId;
            eating.Weight = eatingWeight;

            using (ModelContext mc = new ModelContext())
            {
                mc.Eating.Add(eating);
                mc.SaveChanges();
            }

            EatingUpdate eatingUpdate = new EatingUpdate(eating);
            OnPropertyChanged("CurrentCall");
            OnPropertyChanged("Foods");
            OnPropertyChanged("Eatings");
            return 1;
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

        /// <summary>
        /// Рекомендумое количество калорий
        /// </summary>
        /// <returns></returns>
        public double RecommendKcal
        {
            get 
            {
                CaloriesCalc caloriesCalc = new CaloriesCalc(PanelManager.CurrentUser);
                return Math.Round( caloriesCalc.GetSmallCalories(), 0);
            }
            set { }
        }
    }
}
