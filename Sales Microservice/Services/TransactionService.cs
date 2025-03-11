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
    }
}
