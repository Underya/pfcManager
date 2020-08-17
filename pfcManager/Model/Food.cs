using System;
using System.Collections.Generic;

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

        public virtual ICollection<Eating> Eating { get; set; }
    }
}
