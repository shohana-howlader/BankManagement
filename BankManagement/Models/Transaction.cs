namespace BankManagement.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } // "Deposit", "Withdrawal", "Transfer"
        public DateTime Date { get; set; }
    }
}
