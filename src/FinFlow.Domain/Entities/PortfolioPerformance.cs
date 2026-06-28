namespace FinFlow.Domain.Entities
{
    public class PortfolioPerformance
    {
        public int PortfolioId { get; set; }
        public string PortfolioName { get; set; }
        public decimal TotalReturn { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalReturnPercent { get; set; }
        public string UserName { get; set; }
        public long InvestmentCount { get; set; }
        public decimal AvgDailyChange { get; set; }
    }
}
