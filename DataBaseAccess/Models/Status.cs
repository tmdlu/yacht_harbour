using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Status
    {
        public int IdStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string Conditon { get; set; } = null!;
        public int YachtIdYacht { get; set; }
        public bool SailingPossibility { get; set; }
        public int AccountIdAccount { get; set; }

        public virtual Account AccountIdAccountNavigation { get; set; } = null!;
        public virtual Yacht YachtIdYachtNavigation { get; set; } = null!;
    }
}
