using System;
using System.Collections.Generic;

namespace pfcManager.Model
{
    public partial class Sports
    {
        public long Id { get; set; }
        public float? Att1 { get; set; }
        public float? Att2 { get; set; }
        public float? Att3 { get; set; }
        public float? Att4 { get; set; }
        public long Idusers { get; set; }
        public long Idsports { get; set; }

        public virtual Typesport IdsportsNavigation { get; set; }
        public virtual UsersDB IdusersNavigation { get; set; }
    }
}
