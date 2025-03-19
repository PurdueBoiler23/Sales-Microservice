using Sales_Microservice.dto;
using Sales_Microservice.Models;

namespace Sales_Microservice.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetInventoryAsync();
        Task<Inventory> GetItemByIdAsync(int id);
        Task<Inventory> ReduceInventoryAsync(ReduceInventoryDTO reduceInventoryDTO);
    }
}
