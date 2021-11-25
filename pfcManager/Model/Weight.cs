using System;
using System.Collections.Generic;

namespace pfcManager.Model
{
    public partial class Weight
    {
        public long Id { get; set; }
        public DateTime Datatime { get; set; }
        public float Value { get; set; }
        public long Idusers { get; set; }

        public virtual UsersDB IdusersNavigation { get; set; }
    }
}
