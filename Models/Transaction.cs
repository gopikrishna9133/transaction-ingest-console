namespace TransactionIngest.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string CardNumber { get; set; } = "";
        public string LocationCode { get; set; } = "";
        public string ProductName { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime TransactionTime { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}