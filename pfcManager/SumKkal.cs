using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pfcManager.Model;
using pfcManager.FirstPage;
using System.Collections.ObjectModel;
using System.Threading;

namespace pfcManager
{
    class SumKkal
    {
        /// <summary>
        /// День, за который происходит суммирование 
        /// </summary>
        DateTime day;

        /// <summary>
        /// День, с которым связана сумма
        /// </summary>
        public DateTime Day
        {
            get { return day; }
            set { }
        }

        /// <summary>
        /// Сумма калорий за указанный день
        /// </summary>
        float summ = 0;

        /// <summary>
        /// Сумма калорий за указанный день
        /// </summary>
        public float Summ
        {
            get
            {
                return summ;
            }
            set { }
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        long id = -1;

        /// <summary>
        /// Логин пользователя, для которого считается сумма
        /// </summary>
        public long Id
        {
            get { return id; }
            set { }
        }

        /// <summary>
        /// Класс для подсчёта калорий
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userId"></param>
        public SumKkal(DateTime day, long userId)
        {
            this.day = day;
            this.id = userId;
            summ = CalculationSumm(day, id);
        }

        /// <summary>
        /// Рассчёт суммы калорий за указанный день
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userId"></param>
        float CalculationSumm(DateTime day, long userId)
        {
            //переменная для сохранения суммы калорий
            float summKKal = 0;
            //Получение начала и конца дня
            DateTime startDay = CurrentDate.StartCurrentDay(day),
                endDay = CurrentDate.EndCurrentDay(day);


            using(ModelContext mc = new ModelContext())
            {
                var summ = mc.Eating.Where(o => o.Datatime >= startDay && o.Datatime <= endDay && o.Idusers == userId);

                foreach(Eating eating in summ)
                {
                    EatingUpdate eatingUpdate = new EatingUpdate(eating);
                    summKKal += eatingUpdate.GetResultKkal;
                }
            }

            return summKKal;
        }

        /// <summary>
        /// Получение суммы калорий за каждый день в течение указаного количества дней назад
        /// </summary>
        /// <param name="countDay">Количество дней в течении которого надо получить информацию, начиная с сегодняшнего</param>
        /// <returns></returns>
        public static Collection<SumKkal> CallBackDay(int countDay, long userId)
        {
            DateTime curr = DateTime.Now;

            Collection<SumKkal> sums = new Collection<SumKkal>();

            for(int i = 0; i < countDay; i++)
            {
                SumKkal sumKkal = new SumKkal(curr, userId);
                //Дни, о которых нет информации не учитываются
                if(sumKkal.Summ != 0)
                    sums.Add(sumKkal);

                //Переход к следующему дню
                curr = curr.AddDays(-1);
            }

            return sums;
        }

    }
}
