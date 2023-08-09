using System;
using System.Collections.Generic;

namespace AcmeBank.Persistence.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Person IdNavigation { get; set; } = null!;
}
