using BookTrade.Models;
using System;
using System.Collections.Generic;

namespace BookTrade.BookTradeData.Interfaces
{
    public interface ITradeData
    {
        List<TradeDetails> GetTradeDetails();

        TradeDetails GetTradeDetails(Guid id);

        TradeDetails AddTradeDetails(TradeDetails tradeDetails);

        void DeleteTrade(TradeDetails tradeDetails);

        List<TradeDetails> UserTradeDetailsRequested(Guid fromUserId);

        List<TradeDetails> UserTradeDetailsReceived(Guid fromUserId);

        TradeDetails AcceptTradeDetailsRequest(Guid id);

        TradeDetails CancelTradeDetailsRequest(Guid id);
    }
}
