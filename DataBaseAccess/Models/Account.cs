using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            OrderAccountIdAccount1Navigations = new HashSet<Order>();
            OrderAccountIdAccount2Navigations = new HashSet<Order>();
            OrderAccountIdAccountNavigations = new HashSet<Order>();
            Statuses = new HashSet<Status>();
            FunctionIdFunctions = new HashSet<Function>();
            PowerIdPowers = new HashSet<Power>();
        }

        public int IdAccount { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public byte[]? Photo { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? ClubStatus { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Order> OrderAccountIdAccount1Navigations { get; set; }
        public virtual ICollection<Order> OrderAccountIdAccount2Navigations { get; set; }
        public virtual ICollection<Order> OrderAccountIdAccountNavigations { get; set; }
        public virtual ICollection<Status> Statuses { get; set; }

        public virtual ICollection<Function> FunctionIdFunctions { get; set; }
        public virtual ICollection<Power> PowerIdPowers { get; set; }
    }
}
