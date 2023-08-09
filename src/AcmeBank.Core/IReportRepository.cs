namespace AcmeBank.Contracts
{
    public interface IReportRepository
    {
        Task<List<HistoricalReportData>> GetHistoricalReportAsync(
            int page,
            int perPage,
            DateTime? startDate,
            DateTime? endDate,
            int? customerId,
            CancellationToken cancellationToken);

    }

    public class HistoricalReportData
    {
        public HistoricalReportData()
        {
        }

        public DateTime Date { get; set; }
        public required string CustomerName { get; set; }
        public MovementType AccountType { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

    }

    public enum MovementType
    {
        Savings,
        Checking
    }
}
