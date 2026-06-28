namespace FinFlow.Domain.Entities
{
    public class TransactionTag : BaseEntity
    {
        public string TagName { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
