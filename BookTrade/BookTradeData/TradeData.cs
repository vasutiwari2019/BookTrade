using BookTrade.BookTradeData.Interfaces;
using BookTrade.Data;
using BookTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTrade.BookTradeData
{
    public class TradeData: ITradeData
    {
        private readonly BookTradeContext _bookTradeContext;

        public TradeData(BookTradeContext bookTradeContext)
        {
            _bookTradeContext = bookTradeContext;
        }

        public List<TradeDetails> GetTradeDetails()
        {
            return _bookTradeContext.TradeDetails.ToList();
        }

        public TradeDetails GetTradeDetails(Guid id)
        {
            return _bookTradeContext.TradeDetails.Find(id);
        }

        public TradeDetails AddTradeDetails(TradeDetails tradeDetails)
        {
            var fromUser = _bookTradeContext.Users.Where(x => x.UserId == tradeDetails.FromUserId).FirstOrDefault();
            var toUser = _bookTradeContext.Users.Where(x => x.UserId == tradeDetails.ToUserId).FirstOrDefault();

            var requestedBook = _bookTradeContext.Books.Where(x => x.BookId == tradeDetails.RequestedBookId && x.CreatedByUserId == tradeDetails.ToUserId && !x.IsTraded && x.TradeType == "Traded").FirstOrDefault();

            var tradingBook = _bookTradeContext.Books.Where(x => x.BookId == tradeDetails.TradingBookId && x.CreatedByUserId == tradeDetails.FromUserId && !x.IsTraded && x.TradeType == "Traded").FirstOrDefault();

            if(fromUser != null && toUser != null && requestedBook != null && tradingBook!=null)
            {
                tradeDetails.TradeAccepted = true;
                if (_bookTradeContext.TradeDetails.Where(x => x.RequestedBookId == tradeDetails.RequestedBookId && x.TradingBookId == tradeDetails.TradingBookId && x.FromUserId == tradeDetails.FromUserId && x.ToUserId == tradeDetails.ToUserId).FirstOrDefault() == null)
                {
                    _bookTradeContext.TradeDetails.Add(tradeDetails);
                    _bookTradeContext.SaveChanges();

                    return tradeDetails;
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        public void DeleteTrade(TradeDetails tradeDetails)
        {
            throw new NotImplementedException();
        }

        public List<TradeDetails> UserTradeDetailsRequested(Guid fromUserId)
        {
            return _bookTradeContext.TradeDetails.Where(x => x.FromUserId == fromUserId && x.TradeAccepted && !x.TradeCompleted).ToList();
        }

        public List<TradeDetails> UserTradeDetailsReceived(Guid fromUserId)
        {
            return _bookTradeContext.TradeDetails.Where(x => x.ToUserId == fromUserId && x.TradeAccepted && !x.TradeCompleted).ToList();
        }

        public TradeDetails AcceptTradeDetailsRequest(Guid id)
        {
            var tradeDetails = _bookTradeContext.TradeDetails.Where(x => id == id && x.TradeAccepted && !x.TradeCompleted).FirstOrDefault();

            if (tradeDetails != null)
            {
                tradeDetails.TradeCompleted = true;
                _bookTradeContext.TradeDetails.Update(tradeDetails);
                _bookTradeContext.SaveChanges();
                return tradeDetails;
            }

            return null;
        }

        public TradeDetails CancelTradeDetailsRequest(Guid id)
        {
            var tradeDetails = _bookTradeContext.TradeDetails.Where(x => id == id && x.TradeAccepted && !x.TradeCompleted).FirstOrDefault();

            if (tradeDetails != null)
            {
                tradeDetails.TradeAccepted = false;
                tradeDetails.TradeCompleted = false;

                _bookTradeContext.TradeDetails.Remove(tradeDetails);
                _bookTradeContext.SaveChanges();
                return tradeDetails;
            }

            return null;
        }
    }
}
