using System;
using System.Collections.Generic;

namespace pfcManager.Model
{
    public partial class Typesport
    {
        public Typesport()
        {
            Sports = new HashSet<Sports>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Sports> Sports { get; set; }
    }
}
