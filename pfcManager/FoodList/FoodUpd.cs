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
            this.Eating = food.Eating;
            this.Fats = food.Fats;
            this.Protein = food.Protein;
        }
    }
}
