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
        public List<FindTradeDetails> UserTradeDetailsRequested(Guid fromUserId)
        {
            List <FindTradeDetails> sendTradesList = new List<FindTradeDetails>();

            var tradeFound = _bookTradeContext.TradeDetails.Where(x => x.FromUserId == fromUserId && x.TradeAccepted && !x.TradeCompleted).ToList();

            foreach(var item in tradeFound)
            {
                var fromUser = _bookTradeContext.Users.Find(item.FromUserId);
                var toUser = _bookTradeContext.Users.Find(item.ToUserId);
                var requestedBook = _bookTradeContext.Books.Find(item.RequestedBookId);
                var tradingBook = _bookTradeContext.Books.Find(item.TradingBookId);

                var sendTrade = new FindTradeDetails()
                {
                    TradeId = item.TradeId,
                    FromUser = fromUser,
                    ToUser = toUser,
                    RequestedBook = requestedBook,
                    TradingBook = tradingBook,
                    TradeAccepted = item.TradeAccepted,
                    TradeCompleted = item.TradeCompleted,
                    TradeDelivered = item.TradeDelivered,
                    CreatedDate = item.CreatedDate,
                    ConfirmedDate = item.ConfirmedDate
                };

                sendTradesList.Add(sendTrade);
            }

            return sendTradesList;
        }

        // Method for getting all trades received by User
        public List<FindTradeDetails> UserTradeDetailsReceived(Guid fromUserId)
        {
            List<FindTradeDetails> sendTradesList = new List<FindTradeDetails>();

            var tradeFound = _bookTradeContext.TradeDetails.Where(x => x.ToUserId == fromUserId && x.TradeAccepted && !x.TradeCompleted).ToList();

            foreach (var item in tradeFound)
            {
                var fromUser = _bookTradeContext.Users.Find(item.FromUserId);
                var toUser = _bookTradeContext.Users.Find(item.ToUserId);
                var requestedBook = _bookTradeContext.Books.Find(item.RequestedBookId);
                var tradingBook = _bookTradeContext.Books.Find(item.TradingBookId);

                var sendTrade = new FindTradeDetails()
                {
                    TradeId = item.TradeId,
                    FromUser = fromUser,
                    ToUser = toUser,
                    RequestedBook = requestedBook,
                    TradingBook = tradingBook,
                    TradeAccepted = item.TradeAccepted,
                    TradeCompleted = item.TradeCompleted,
                    TradeDelivered = item.TradeDelivered,
                    CreatedDate = item.CreatedDate,
                    ConfirmedDate = item.ConfirmedDate
                };

                sendTradesList.Add(sendTrade);
            }

            return sendTradesList;
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

                var fromUserBookID = tradeDetails.TradingBookId;
                var toUserBookID = tradeDetails.RequestedBookId;

                var fromUser = _bookTradeContext.Books.Where(x=> x.BookId == fromUserBookID).FirstOrDefault();
                var toUser = _bookTradeContext.Books.Where(x => x.BookId == toUserBookID).FirstOrDefault();

                fromUser.IsTraded = true;
                toUser.IsTraded = true;
                _bookTradeContext.Books.Update(fromUser);
                _bookTradeContext.Books.Update(toUser);
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
