using BookTrade.BookTradeData.Interfaces;
using BookTrade.Data;
using BookTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTrade.BookTradeData
{
    // TradeData class for handling trade related operations
    public class TradeData: ITradeData
    {
        #region Global Variables
        private readonly BookTradeContext _bookTradeContext;
        #endregion

        #region Constructor
        public TradeData(BookTradeContext bookTradeContext)
        {
            _bookTradeContext = bookTradeContext;
        }
        #endregion

        #region Pubic Methods

        // Method for getting all trades
        public List<TradeDetails> GetTradeDetails()
        {
            return _bookTradeContext.TradeDetails.ToList();
        }

        // Method for getting specified trade
        public TradeDetails GetTradeDetails(Guid id)
        {
            return _bookTradeContext.TradeDetails.Find(id);
        }

        // Method for placing a new trade
        public TradeDetails AddTradeDetails(TradeDetails tradeDetails)
        {
            var fromUser = _bookTradeContext.Users.Where(x => x.UserId == tradeDetails.FromUserId).FirstOrDefault();
            var toUser = _bookTradeContext.Users.Where(x => x.UserId == tradeDetails.ToUserId).FirstOrDefault();

            var requestedBook = _bookTradeContext.Books.Where(x => x.BookId == tradeDetails.RequestedBookId && x.CreatedByUserId == tradeDetails.ToUserId && !x.IsTraded).FirstOrDefault();

            var tradingBook = _bookTradeContext.Books.Where(x => x.BookId == tradeDetails.TradingBookId && x.CreatedByUserId == tradeDetails.FromUserId && !x.IsTraded).FirstOrDefault();

            if(fromUser != null && toUser != null && requestedBook != null && tradingBook!=null)
            {
                tradeDetails.TradeAccepted = true;
                tradeDetails.CreatedDate = DateTime.Now;
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

        // Method for deleting a trade
        public void DeleteTrade(TradeDetails tradeDetails)
        {
            throw new NotImplementedException();
        }

        // Method for getting all trades created by User
        public List<TradeDetails> UserTradeDetailsRequested(Guid fromUserId)
        {
            return _bookTradeContext.TradeDetails.Where(x => x.FromUserId == fromUserId && x.TradeAccepted && !x.TradeCompleted).ToList();
        }

        // Method for getting all trades received by User
        public List<TradeDetails> UserTradeDetailsReceived(Guid fromUserId)
        {
            return _bookTradeContext.TradeDetails.Where(x => x.ToUserId == fromUserId && x.TradeAccepted && !x.TradeCompleted).ToList();
        }

        // Method for accepting trade request
        public TradeDetails AcceptTradeDetailsRequest(Guid id)
        {
            var tradeDetails = _bookTradeContext.TradeDetails.Where(x => x.TradeId == id && x.TradeAccepted && !x.TradeCompleted).FirstOrDefault();

            if (tradeDetails != null)
            {
                tradeDetails.TradeCompleted = true;
                tradeDetails.ConfirmedDate = DateTime.Now;
                _bookTradeContext.TradeDetails.Update(tradeDetails);
                _bookTradeContext.SaveChanges();
                return tradeDetails;
            }

            return null;
        }

        // Method for cancelling trade request
        public TradeDetails CancelTradeDetailsRequest(Guid id)
        {
            var tradeDetails = _bookTradeContext.TradeDetails.Where(x => x.TradeId == id && x.TradeAccepted && !x.TradeCompleted).FirstOrDefault();

            if (tradeDetails != null)
            {
                tradeDetails.TradeAccepted = false;
                tradeDetails.TradeCompleted = false;
                tradeDetails.ConfirmedDate= DateTime.Now;

                _bookTradeContext.TradeDetails.Remove(tradeDetails);
                _bookTradeContext.SaveChanges();
                return tradeDetails;
            }

            return null;
        }
        #endregion
    }
}
