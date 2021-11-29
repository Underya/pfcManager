using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace pfcManager.Model
{
    public partial class Users
    {
        public Users()
        {
            Eating = new HashSet<Eating>();
            Sports = new HashSet<Sports>();
            Weight = new HashSet<Weight>();
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Midname { get; set; }
        public char? Sex { get; set; }
        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        public DateTime? Datebridh { get; set;}
        /// <summary>
        /// Рост пользователя
        /// </summary>
        public float? Height { get; set; }

        public virtual ICollection<Eating> Eating { get; set; }
        public virtual ICollection<Sports> Sports { get; set; }
        public virtual ICollection<Weight> Weight { get; set; }

    }
}
