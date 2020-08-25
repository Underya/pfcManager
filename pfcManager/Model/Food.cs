using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace pfcManager.Model
{
    public partial class Food
    {
        public Food()
        {
            Eating = new HashSet<Eating>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public float Kkal { get; set; }
        public string Description { get; set; }
        public float? Protein { get; set; }
        public float? Fats { get; set; }
        public float? Carbohydrates { get; set; }

        /// <summary>
        /// Вся еда из бд отсортированная по алфавиту
        /// </summary>
        /// <returns></returns>
        public static Collection<Food> GetAllFood()
        {
            Collection<Food> AllFood = new Collection<Food>();
            using (ModelContext mc = new ModelContext())
            {
                var foods = mc.Food.OrderBy(o => o.Name);
                foreach (Food food in foods)
                {
                    AllFood.Add(food);
                }
                
            }

            return AllFood;
        }

        public virtual ICollection<Eating> Eating { get; set; }
    }
}
