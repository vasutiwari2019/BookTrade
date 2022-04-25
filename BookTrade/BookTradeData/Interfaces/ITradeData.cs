using BookTrade.Models;
using System;
using System.Collections.Generic;

namespace BookTrade.BookTradeData.Interfaces
{
    // TradeData Interface
    public interface ITradeData
    {
        #region Methods
        List<TradeDetails> GetTradeDetails();

        TradeDetails GetTradeDetails(Guid id);

        TradeDetails AddTradeDetails(TradeDetails tradeDetails);

        void DeleteTrade(TradeDetails tradeDetails);

        List<FindTradeDetails> UserTradeDetailsRequested(Guid fromUserId);

        List<FindTradeDetails> UserTradeDetailsReceived(Guid fromUserId);

        TradeDetails AcceptTradeDetailsRequest(Guid id);

        TradeDetails CancelTradeDetailsRequest(Guid id);
        #endregion
    }
}
