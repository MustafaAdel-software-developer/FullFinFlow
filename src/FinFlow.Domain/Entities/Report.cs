namespace FinFlow.Domain.Entities
{
    public class Report : BaseEntity
    {
        public string ReportType { get; set; }
        public string ReportName { get; set; }
        public string FilePath { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public User User { get; set; }
        public Currency Currency { get; set; }
    }
}
