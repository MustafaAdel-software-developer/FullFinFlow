namespace FinFlow.Domain.Entities
{
    public class TransactionStatus : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
