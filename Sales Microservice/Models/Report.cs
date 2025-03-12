namespace Sales_Microservice.Models
{
    public class Report
    {
        public int Year { get; set; }
        public string PeriodType { get; set; } // i.e. year
        public string PeriodName { get; set; } // i.e. 2024 
        public double NetProfit { get; set; }
        public double AverageTransactionValue { get; set; }
        public int TotalTransactions { get; set; }
    }
}
