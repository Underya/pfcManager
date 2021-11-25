using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace pfcManager.Model
{
    public partial class Eating
    {
        public long Id { get; set; }
        public DateTime Datatime { get; set; }
        public float Weight { get; set; }
        public long Idusers { get; set; }
        public long Idfood { get; set; }

        /// <summary>
        /// Получение всех приёмов пищи за указанный день
        /// </summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        public static List<Eating> GetEatings(DateTime Day)
        {
            List<Eating> eatings = new List<Eating>();

            //Загрузка всех приёмов пищи
            //Получение времени
            DateTime start = CurrentDate.StartCurrentDay(Day), end = CurrentDate.EndCurrentDay(Day);

            //Загрузка всех значений
            using(ModelContext mc = new ModelContext())
            {
               eatings = mc.Eating.Where(o => o.Datatime > start && o.Datatime < end).OrderBy(o => o.Datatime).ToList();
            }

            return eatings;
        }

        public virtual Food IdfoodNavigation { get; set; }
        public virtual UsersDB IdusersNavigation { get; set; }
    }
}
