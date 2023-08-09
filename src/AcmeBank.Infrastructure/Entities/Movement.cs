namespace AcmeBank.Persistence.Entities;

public partial class Movement
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int Type { get; set; }

    public decimal Amount { get; set; }

    public decimal Balance { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
