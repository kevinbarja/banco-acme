using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcmeBank.Persistence.Repositories
{
    public class ReportRepository : IReportRepository
    {
        protected readonly AcmeBankDbContext _dbContext;

        public ReportRepository(AcmeBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<HistoricalReportData>> GetHistoricalReportAsync(
            int page,
            int perPage,
            DateTime? startDate,
            DateTime? endDate,
            int? customerId,
            CancellationToken cancellationToken)
        {
            IQueryable<Movement> query = _dbContext.Movements.Include(m => m.Account).ThenInclude(a => a.Customer);

            if (startDate is not null && endDate is not null)
            {
                query = query.Where(m => m.Date >= startDate && m.Date <= endDate);
            }

            if (customerId is not null)
            {
                query = query.Where(m => m.Account.Customer.Id == customerId);
            }

            query = query.OrderByDescending(m => m.Date);
            var movements = await query.Skip((page - 1) * perPage).Take(perPage)
            .AsNoTracking()
            .Select(m => new HistoricalReportData
            {
                Date = m.Date,
                CustomerName = m.Account.Customer.FullName,
                AccountType = (MovementType)m.Account.Type,
                InitialBalance = m.InitialBalance,
                Amount = m.Amount,
                Balance = m.Balance
            }).ToListAsync(cancellationToken);
            return movements;
        }
    }
}
