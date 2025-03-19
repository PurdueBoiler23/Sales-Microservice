using Sales_Microservice.Models;

namespace Sales_Microservice.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Report>> GetAllReportsAsync();
        Task<Transaction> GetTransactionAsync(int transactionId);
    }
}
