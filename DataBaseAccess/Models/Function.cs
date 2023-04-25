using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Function
    {
        public Function()
        {
            AccountIdAccounts = new HashSet<Account>();
        }

        public int IdFunction { get; set; }
        public string? FunctionName { get; set; }

        public virtual ICollection<Account> AccountIdAccounts { get; set; }
    }
}
