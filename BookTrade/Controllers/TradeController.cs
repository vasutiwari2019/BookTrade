using BookTrade.BookTradeData.Interfaces;
using BookTrade.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookTrade.Controllers
{
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeData _tradeData;

        public TradeController(ITradeData tradeData)
        {
            _tradeData = tradeData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetTrades()
        {
            return Ok(_tradeData.GetTradeDetails());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetTrades(Guid id)
        {
            var trades = _tradeData.GetTradeDetails(id);
            if (trades != null)
                return Ok(trades);
            else
                return NotFound($"Trade with Id:{id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddTrades(TradeDetails tradeDetails)
        {
            var tradeCreated = _tradeData.AddTradeDetails(tradeDetails);

            if(tradeCreated != null)
            {
                return Ok(tradeCreated);
            }

            else
            {
                return BadRequest("Trade Not Created");
            }
        }

        [HttpGet]
        [Route("api/[controller]/usertraderequested/{fromUserId}")]
        public IActionResult UserTradeRequested(Guid fromUserId)
        {
            var tradeDetails = _tradeData.UserTradeDetailsRequested(fromUserId);

            if (tradeDetails != null)
                return Ok(tradeDetails);
            else
            {
                return BadRequest("No Trades for User");
            }
        }

        [HttpGet]
        [Route("api/[controller]/usertradereceived/{fromUserId}")]
        public IActionResult UserTradeReceived(Guid fromUserId)
        {
            var tradeDetails = _tradeData.UserTradeDetailsReceived(fromUserId);

            if (tradeDetails != null)
                return Ok(tradeDetails);
            else
            {
                return BadRequest("No Trades for User");
            }
        }

        [HttpGet]
        [Route("api/[controller]/accepttraderequest/{id}")]
        public IActionResult AcceptTradeRequest(Guid id)
        {
            var tradeDetails = _tradeData.AcceptTradeDetailsRequest(id);

            if (tradeDetails != null)
                return Ok(tradeDetails);
            else
            {
                return BadRequest("Trade details not found");
            }
        }

        [HttpGet]
        [Route("api/[controller]/canceltraderequest/{id}")]
        public IActionResult CancelTradeRequest(Guid id)
        {
            var tradeDetails = _tradeData.CancelTradeDetailsRequest(id);

            if (tradeDetails != null)
                return Ok(tradeDetails);
            else
            {
                return BadRequest("Trade details not found");
            }
        }
    }
}
