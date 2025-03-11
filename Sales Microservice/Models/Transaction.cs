namespace Sales_Microservice.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public int CustomerId { get; set; }
        public double TransactionAmount { get; set; }
    }
}
