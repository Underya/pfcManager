using System;
using System.Collections.Generic;
using System.Text;

namespace pfcManager
{
    /// <summary>
    /// Класс представляет методы для получения специальных дат
    /// </summary>
    static class CurrentDate
    {
        /// <summary>
        /// Метод возвращает время начало сегодняшнего дня
        /// </summary>
        /// <returns></returns>
        public static DateTime StartThisDay()
        {
            return StartCurrentDay(DateTime.Now);
        }

        /// <summary>
        //  Получение начала указанного дня
        /// </summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        public static DateTime StartCurrentDay(DateTime Day)
        {
            DateTime CurrentTime = Day;
            DateTime time = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, 0, 0, 0);
            return time;
        }

        /// <summary>
        /// Получение начала сегодняшнего дня
        /// </summary>
        /// <returns></returns>
        public static DateTime EndThisDay()
        {
            return EndCurrentDay(DateTime.Now);
        }

        /// <summary>
        /// Получение времени конца указанного дня
        /// </summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        public static DateTime EndCurrentDay(DateTime Day)
        {
            DateTime CurrentTime = Day;
            DateTime time = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, 23, 59, 59);
            return time;
        }
    }
}
