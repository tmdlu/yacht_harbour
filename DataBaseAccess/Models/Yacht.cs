using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Yacht
    {
        public Yacht()
        {
            Orders = new HashSet<Order>();
            Statuses = new HashSet<Status>();
        }

        public int IdYacht { get; set; }
        public string YachtName { get; set; } = null!;
        public string Type { get; set; } = null!;
        public float Length { get; set; }
        public float Width { get; set; }
        public float Draft { get; set; }
        public float SailedSurface { get; set; }
        public int CrewNumber { get; set; }
        public string? Remarks { get; set; }
        public bool IsAvailable { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Status> Statuses { get; set; }
    }
}
