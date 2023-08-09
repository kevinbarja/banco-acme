namespace AcmeBank.Api.Endpoints.Accounts.Movements
{
    public class CreateMovementResult
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public MovementType Type { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public int AccountId { get; set; }
    }

    public enum MovementType
    {
        Debit,
        Credit,
    }
}
