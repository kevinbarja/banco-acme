using AcmeBank.Api.Pagging;

namespace AcmeBank.Api.Endpoints.Reports
{
    public class HistoricalReportRequest : PagedRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CustomerId { get; set; }
    }
}
