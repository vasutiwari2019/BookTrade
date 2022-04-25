namespace BookTrade.Models
{
    public class FindTradeDetails
    {
        public User FromUser { get; set; }

        public User ToUser { get; set; }

        public Book RequestedBook { get; set; }

        public Book TradingBook { get; set; }
    }
}
