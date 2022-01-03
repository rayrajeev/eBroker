
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using eBroker.Service;
using eBroker.Models;
using eBroker.Helper;

namespace eBroker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraderController : ControllerBase
    {

        private readonly ITradeService _tradeService;

        private readonly ITradeHelperWrapper _tradeHelperWrapper;

        public TraderController(ITradeService tradeService, ITradeHelperWrapper tradeHelperWrapper)
        {
            _tradeService = tradeService;
            _tradeHelperWrapper = tradeHelperWrapper;
        }

        [HttpPost("addfund")]
        public async Task<IActionResult> AddFund(AddFund addFund)
        {
            if (addFund.TraderId <= 0 || addFund.Funds <= 0)
            {
                return BadRequest(TradeConstant.TraderOrFundNotValid);
            }

            var result = await _tradeService.AddFunds(addFund.TraderId, addFund.Funds);
            if (!result.Contains("Error"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell([FromBody] TradingData tradingData)
        {
            if (tradingData.Quantity <= 0 || tradingData.TraderId <= 0 || string.IsNullOrWhiteSpace(tradingData.EquityName))
            {
                return BadRequest(TradeConstant.InvalidTradeData);
            }

            if (!_tradeHelperWrapper.IsValidTradeTime())
            {
                return BadRequest(TradeConstant.TradeTime);
            }

            var result = await _tradeService.SellEquity(tradingData.TraderId, tradingData.EquityName, tradingData.Quantity);
            if (!result.Contains("Error"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy([FromBody] TradingData tradingData)
        {
            if (tradingData.Quantity <= 0 || tradingData.TraderId <= 0 || string.IsNullOrWhiteSpace(tradingData.EquityName))
            {
                return BadRequest(TradeConstant.InvalidTradeData);
            }

            if (!_tradeHelperWrapper.IsValidTradeTime())
            {
                return BadRequest(TradeConstant.TradeTime);
            }

            var result = await _tradeService.BuyEquity(tradingData.TraderId, tradingData.EquityName, tradingData.Quantity);
            if (!result.Contains("Error"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
