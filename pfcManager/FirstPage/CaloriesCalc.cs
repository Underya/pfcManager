using System;
using System.Collections.Generic;
using System.Text;
using pfcManager.Model;
using System.Linq;

namespace pfcManager.FirstPage
{
    /// <summary>
    /// Возможный пол
    /// </summary>
    enum Sex { Male, Female};

    /// <summary>
    /// Рассчёт норм калорий для пользователй
    /// </summary>
    class CaloriesCalc
    {
        /// <summary>
        /// Рост пользоваотеля
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Количест лет пользователя
        /// </summary>
        public int YearOld { get; set; }

        /// <summary>
        /// Вес пользователя
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Пол пользователя
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// Указание свойств через поля
        /// </summary>
        public CaloriesCalc()
        {
            Height = 175;
            YearOld = 25;
            Weight = 70;
            Sex = Sex.Male;
        }

        /// <summary>
        /// Заполнение свойств с помощью пользователя
        /// </summary>
        /// <param name="user"></param>
        public CaloriesCalc(UserSave user)
        {
            if (user.Height != null)
                Height = (float)user.Height;
            else
                Height = 175;

            YearOld = user.YearsOld;

            if (user.Sex == 'f')
                Sex = Sex.Female;
            else
                Sex = Sex.Male;

            Weight = GetActualWeight(user.Id);
        }

        /// <summary>
        /// Получение последнего веса пользоваотеля
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        float GetActualWeight(long userID)
        {
            float actualWeigt = 70;

            using(ModelContext mc = new ModelContext())
            {
                Model.Weight weightObj = mc.Weight.Where(o => o.Idusers == userID).OrderByDescending(o => o.Datatime).FirstOrDefault();
                //Если нет информацации 
                if (weightObj != null)
                    actualWeigt = weightObj.Value;
            }

            return actualWeigt;
        }

        /// <summary>
        /// Получение нормы калорий
        /// </summary>
        /// <returns></returns>
        public float GetNormCalories()
        {
            if (Sex == Sex.Male)
                return ((10.0F * Weight) + (6.25F * Height) - (5 * YearOld) + 5) * 1.2F;
            else
                return ((10.0F * Weight) + (6.25F * Height) - (5 * YearOld) -161) * 1.2F;
        }

        /// <summary>
        /// Получение нормы калорий для похудания
        /// </summary>
        /// <returns></returns>
        public float GetSmallCalories()
        {
            return 0.8F * GetNormCalories();
        }
    }

}
