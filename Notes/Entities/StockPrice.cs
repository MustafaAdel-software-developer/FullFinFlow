namespace FinFlow.Domain.Entities
{
    public class StockPrice : BaseEntity
    {
        public string Symbol { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public int Volume { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
