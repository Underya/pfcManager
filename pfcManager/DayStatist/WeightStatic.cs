using pfcManager.Model;
using pfcManager.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace pfcManager.DayStatist
{
    /// <summary>
    /// Класс позволяет получить статистику по весу за послендние n в виде средних значений
    /// </summary>
    class WeightStatic
    {
        public static Collection<Weight> GetWeights(long UserId, int countDay =30)
        {
            
            List<Weight> allWeigthInPeriod = GetWeightInPeriod(UserId, countDay);

            Collection<Weight> WeightToDay = ClearRepeatWeight(allWeigthInPeriod, countDay);

            return WeightToDay;
        }

        private static List<Weight> GetWeightInPeriod(long UserId, int countDay)
        {
            DateTime StartPeriod = DateTime.Now.AddDays(-countDay);
            using (ModelContext mc = new ModelContext())
                return mc.Weight.
                    Where(weight => 
                        weight.Idusers == UserId 
                        && weight.Datatime >= StartPeriod).
                        ToList();
        }
        private static Collection<Weight> ClearRepeatWeight(List<Weight> allWeigthInPeriod, int countDay)
        {
            Collection<Weight> WeightToDay = new Collection<Weight>();

            DateTime currDay = DateTime.Now.Date;
            for (int i = 0; i < countDay; i++)
            {
                var weight = GetWeightWithoutReplace(allWeigthInPeriod, currDay);
                if (weight != null)
                    WeightToDay.Add(weight);
                currDay = currDay.AddDays(-1);
            }

            return WeightToDay;
        }

        private static Weight GetWeightWithoutReplace(List<Weight> allWeigthInPeriod, DateTime currDay)
        {
            using (ModelContext mc = new ModelContext())
                return allWeigthInPeriod.Where(o => IsSameDay(o, currDay)).OrderByDescending(o => o.Datatime).FirstOrDefault();
        }

        private static bool IsSameDay(Weight o, DateTime currentDay)
        {
            var WeightDay = o.Datatime;
            return WeightDay.Year == currentDay.Year 
                && WeightDay.Month == currentDay.Month 
                && WeightDay.Day == currentDay.Day;
        }
    }
}
