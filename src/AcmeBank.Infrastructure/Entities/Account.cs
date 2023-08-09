namespace AcmeBank.Persistence.Entities;

public partial class Account
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public short Type { get; set; }

    public decimal InitialBalance { get; set; }

    public bool Status { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
}
