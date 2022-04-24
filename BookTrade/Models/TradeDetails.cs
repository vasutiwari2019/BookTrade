using System;
using System.ComponentModel.DataAnnotations;

namespace BookTrade.Models
{
    public class TradeDetails
    {
        [Key]
        public Guid TradeId { get; set; }

        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public Guid RequestedBookId { get; set; }

        public Guid TradingBookId { get; set; }
        
        public bool TradeAccepted { get; set; }

        public bool TradeCompleted { get; set; }
    }
}
