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
            Collection<Weight> newCollection = new Collection<Weight>();

            DateTime currDay = DateTime.Now;

            for(int i = 0; i < countDay; i++)
            {
                DateTime start = CurrentDate.StartCurrentDay(currDay), end = CurrentDate.EndCurrentDay(currDay);
                using (ModelContext mc = new ModelContext()) 
                {
                    List<Weight> AllWeightForDay = mc.Weight.Where(o => o.Datatime >= start && o.Datatime <= end && o.Idusers == UserId).ToList();
                    if(AllWeightForDay.Count != 0)
                        newCollection.Add(GetMean(AllWeightForDay));
                }
                currDay = currDay.AddDays(-1);
            }

            return newCollection;
        }

        /// <summary>
        /// Получение среднего значения для дня из указаного списка
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        static Weight GetMean(List<Weight> weights)
        {
            //Если 1 значение, то оно возвращается 
            if (weights.Count == 1)
                return weights[0];

            float Summ = 0;
            int count = 0;

            foreach(Weight weight in weights)
            {
                Summ += weight.Value;
                count++;
            }

            Weight resweight = new Weight();
            resweight.Value = Summ / (float)count;
            //Округление веса
            resweight.Value = (float)Math.Round((double)resweight.Value, 2);
            resweight.Datatime = weights[0].Datatime;
            resweight.Idusers = weights[0].Idusers;

            return resweight;
        }
    }
}
