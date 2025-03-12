using Microsoft.EntityFrameworkCore;
using Sales_Microservice.Models;
using Sales_Microservice.Services.Interfaces;

namespace Sales_Microservice.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionDbContext _context;

        public TransactionService(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<List<Report>> GetAllReportsAsync()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            var reports = new List<Report>();

            // Group by Year
            reports.AddRange(transactions
                .GroupBy(t => t.DateOfTransaction.Year)
                .Select(g => new Report
                {
                    Year = g.Key,
                    PeriodType = "Year",
                    PeriodName = g.Key.ToString(),
                    NetProfit = Math.Round(g.Sum(t => t.TransactionAmount), 2),
                    AverageTransactionValue = Math.Round(g.Average(t => t.TransactionAmount), 2),
                    TotalTransactions = g.Count()
                }));

            // Group by Quarter
            reports.AddRange(transactions
                .GroupBy(t => new { t.DateOfTransaction.Year, Quarter = (t.DateOfTransaction.Month - 1) / 3 + 1 })
                .Select(g => new Report
                {
                    Year = g.Key.Year,
                    PeriodType = "Quarter",
                    PeriodName = $"Q{g.Key.Quarter} {g.Key.Year}",
                    NetProfit = Math.Round(g.Sum(t => t.TransactionAmount), 2),
                    AverageTransactionValue = Math.Round(g.Average(t => t.TransactionAmount), 2),
                    TotalTransactions = g.Count()
                }));

            // Group by Month
            reports.AddRange(transactions
                .GroupBy(t => new { t.DateOfTransaction.Year, t.DateOfTransaction.Month })
                .Select(g => new Report
                {
                    Year = g.Key.Year,
                    PeriodType = "Month",
                    PeriodName = $"{new DateTime(g.Key.Year, g.Key.Month, 1):MMMM yyyy}",
                    NetProfit = Math.Round(g.Sum(t => t.TransactionAmount), 2),
                    AverageTransactionValue = Math.Round(g.Average(t => t.TransactionAmount), 2),
                    TotalTransactions = g.Count()
                }));

            return reports
                .OrderBy(r => r.Year)
                .ThenBy(r => r.PeriodType == "Year" ? 0 : (r.PeriodType == "Quarter" ? 1 : 2)) // Year first, then Quarter, then Month
                .ThenBy(r => r.PeriodType == "Quarter" ? int.Parse(r.PeriodName.Split(' ')[0].Substring(1)) : 0) // Sort Q1, Q2, Q3, Q4
                .ThenBy(r => r.PeriodType == "Month" ? DateTime.ParseExact(r.PeriodName, "MMMM yyyy", null).Month : 0) // Sort January - December
                .ToList();
        }
    }
}
