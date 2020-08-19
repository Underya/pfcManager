using System;
using System.Collections.Generic;
using System.Text;

namespace pfcManager
{
    /// <summary>
    /// Класс представляет методы для получения специальных дат
    /// </summary>
    class CurrentDate
    {
        /// <summary>
        /// Метод возвращает время начало сегодняшнего дня
        /// </summary>
        /// <returns></returns>
        public static DateTime StartThisDay()
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime time = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, 0, 0, 0);
            return time;
        }

        public static DateTime EndThisDay()
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime time = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, 23, 59, 59);
            return time;
        }
    }
}
