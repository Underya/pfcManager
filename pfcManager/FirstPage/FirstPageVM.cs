using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using pfcManager.Model;

namespace pfcManager.FirstPage
{
    /// <summary>
    /// VM для превой страницы с общей информацией
    /// </summary>
    class FirstPageVM
    {
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
                    var auto = mc.Eating.Where(o => o.Datatime > start && o.Datatime < end);
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

        double CheckVal = 124.4;

        public double CurrentWeight
        {
            get { return CheckVal; }
            set { CheckVal = value; }
        }
    }
}
