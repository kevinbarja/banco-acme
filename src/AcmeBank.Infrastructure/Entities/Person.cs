using System;
using System.Collections.Generic;

namespace AcmeBank.Persistence.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public short Gender { get; set; }

    public short Age { get; set; }

    public int IdentityNumber { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual Customer? Customer { get; set; }
}
