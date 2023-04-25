using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Power
    {
        public Power()
        {
            AccountIdAccounts = new HashSet<Account>();
        }

        public int IdPower { get; set; }
        public string? PowerName { get; set; }

        public virtual ICollection<Account> AccountIdAccounts { get; set; }
    }
}
