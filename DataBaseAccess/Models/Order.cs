using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Order
    {
        public int IdOrder { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsReleased { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AccountIdAccount { get; set; }
        public int YachtIdYacht { get; set; }
        public int? AccountIdAccount1 { get; set; }
        public int? AccountIdAccount2 { get; set; }
        public bool IsReturned { get; set; }
        public string? OrderType { get; set; }

        public virtual Account? AccountIdAccount1Navigation { get; set; }
        public virtual Account? AccountIdAccount2Navigation { get; set; }
        public virtual Account AccountIdAccountNavigation { get; set; } = null!;
        public virtual Yacht YachtIdYachtNavigation { get; set; } = null!;
    }
}
