namespace FinFlow.Domain.Entities
{
    public class PlaidAccount : BaseEntity
    {
        public int PlaidItemId { get; set; }
        public string EncryptedPlaidAccessToken { get; set; }
        public string PlaidAccountId { get; set; }
        public string InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public DateTime? LastSync { get; set; }
        public bool IsConnected { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
