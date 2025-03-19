namespace Sales_Microservice.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Category {  get; set; }
        public int QuantityAvailable { get; set; }
    }
}
