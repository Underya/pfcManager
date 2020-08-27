using System;
using System.Collections.Generic;
using System.Text;
using pfcManager.Model;

namespace pfcManager.FoodList
{
    /// <summary>
    ///  Модель еды с автобновлением некоторых парамтеров
    /// </summary>
    class FoodUpd : Food
    {
        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        public FoodUpd()
        {

        }

        /// <summary>
        /// Конструктор для трансформации из базового типа в обновляемый
        /// </summary>
        /// <param name="food"></param>
        public FoodUpd(Food food)
        {
            //Сохранение всех параметров
            this.Id = food.Id;
            this.Name = food.Name;
            this.Kkal = food.Kkal;
            this.Description = food.Description;
            this.Carbohydrates = food.Carbohydrates;
            if (Carbohydrates == null)
                Carbohydrates = 0;
            this.Eating = food.Eating;
            this.Fats = food.Fats;
            if (Fats == null)
                Fats = 0;
            this.Protein = food.Protein;
            if(Protein == null)
                Protein = 0;
        }
    }
}
