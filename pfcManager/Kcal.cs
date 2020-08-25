using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace pfcManager
{
    /// <summary>
    /// Ккалории
    /// </summary>
    class Kcal
    {
        /// <summary>
        /// Норма ккал на 100 грамм для данного подсчёта
        /// </summary>
        public double KcalNorm { get; set; }

        /// <summary>
        /// Вес рассчитываем
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Результат вычислений
        /// </summary>
        public double Summ 
        {
            get 
            {
                return Math.Round( Weight * KcalNorm / 100.0, 1);
            }
        }

        /// <summary>
        /// Рассчёт каллорийности
        /// </summary>
        /// <param name="kcalNorm">Норма на 100 грамм</param>
        /// <param name="weight">Вес</param>
        public Kcal(double kcalNorm, double weight)
        {
            KcalNorm = kcalNorm;
            Weight = weight;
        }

        /// <summary>
        /// Преобразования результата к типу doubl
        /// </summary>
        /// <param name="Kcal"></param>
        public static implicit operator double(Kcal Kcal) 
        {
            return Kcal.Summ;
        }

        /// <summary>
        /// Преобразование к типу строки
        /// </summary>
        /// <param name="kcal"></param>
        public static implicit operator string(Kcal kcal)
        {
            return kcal.Summ.ToString();
        }
    }
}
