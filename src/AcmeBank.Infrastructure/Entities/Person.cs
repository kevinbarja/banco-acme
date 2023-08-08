using System;
using System.Collections.Generic;

namespace AcmeBank.Persistence.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int Gender { get; set; }

    public int Age { get; set; }

    public int IdentityNumber { get; set; }

    public string? Address { get; set; }

    public int? PhoneNumber { get; set; }

    public virtual Customer? Customer { get; set; }
}
