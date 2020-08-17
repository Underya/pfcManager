using System;
using System.Collections.Generic;

namespace pfcManager.Model
{
    public partial class Eating
    {
        public long Id { get; set; }
        public DateTime Datatime { get; set; }
        public float Weight { get; set; }
        public long Idusers { get; set; }
        public long Idfood { get; set; }

        public virtual Food IdfoodNavigation { get; set; }
        public virtual Users IdusersNavigation { get; set; }
    }
}
