using System;

namespace BookTrade.Models
{
    public class FindTradeDetails
    {
        public Guid TradeId { get; set; }
        public User FromUser { get; set; }

        public User ToUser { get; set; }

        public Book RequestedBook { get; set; }

        public Book TradingBook { get; set; }
        public bool TradeAccepted { get; set; }

        public bool TradeCompleted { get; set; }

        public bool TradeDelivered { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ConfirmedDate { get; set; }
    }
}
