using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sales_Microservice.Data;
using Sales_Microservice.dto;
using Sales_Microservice.Models;
using Sales_Microservice.Services.Interfaces;

namespace Sales_Microservice.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryDbContext _context;

        public InventoryService(InventoryDbContext inventoryDbContext)
        {
            _context = inventoryDbContext;
        }

        public async Task<List<Inventory>> GetInventoryAsync()
        {
            return await _context.Inventory.ToListAsync();
        }

        public async Task<Inventory> GetItemByIdAsync(int inventoryId)
        {
            return await _context.Inventory.FindAsync(inventoryId);
        }

        public async Task<Inventory> ReduceInventoryAsync(ReduceInventoryDTO reduceInventoryDTO)
        {
            var inventory = await _context.Inventory.FindAsync(reduceInventoryDTO.Id);
            if (inventory == null) { throw new Exception("Item not found."); }
            if (inventory.QuantityAvailable > reduceInventoryDTO.quantity)
            {
                inventory.QuantityAvailable = inventory.QuantityAvailable - reduceInventoryDTO.quantity;
            }
            else
            {
                throw new Exception("Error, quantity requested is larger than quantity available.");
            }

            await _context.SaveChangesAsync();

            return inventory;
        }
    }
}
