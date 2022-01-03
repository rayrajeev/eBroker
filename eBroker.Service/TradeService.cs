using System;
using System.Threading.Tasks;
using eBroker.DataAccess.Repository;
using System.Linq;

namespace eBroker.Service
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<string> BuyEquity(int traderId, string equiryName, int quantity)
        {
            var equity = await _tradeRepository.GetEquityByName(equiryName);
            if (equity == null)
            {
                return TradeConstant.EquityNotListedError;
            }
            var trader = await _tradeRepository.GetTraderById(traderId);
            if (trader == null)
            {
                return TradeConstant.TraderNotRegisteredError;
            }

            var totalEquityCost = quantity * equity.UnitPrice;

            if (trader.Funds < totalEquityCost)
            {
                return TradeConstant.NotEnoughFundError;
            }
            await _tradeRepository.Buy(traderId, equiryName, quantity);
            return TradeConstant.OrderSuccess;
        }



        public async Task<string> SellEquity(int traderId, string equiryName, int quantity)
        {

            var equity = await _tradeRepository.GetEquityByName(equiryName);
            if (equity == null)
            {
                return TradeConstant.EquityNotListedError;
            }
            var trader = await _tradeRepository.GetTraderById(traderId);
            if (trader == null)
            {
                return TradeConstant.TraderNotRegisteredError;
            }
            var traderHoldings = trader.TradeHoldings.FirstOrDefault(x => x.EquityId == equity.Id);
            if (traderHoldings == null || traderHoldings.Units < quantity)
            {
                return TradeConstant.TradeHoldingError;
            }

            var totalEquityCost = quantity * equity.UnitPrice;
            var brokrage = Math.Max(totalEquityCost * 0.05 / 100, 20.00);
            await _tradeRepository.Sell(traderId, equiryName, quantity, brokrage);

            return TradeConstant.OrderSuccess;
        }

        public async Task<string> AddFunds(int traderId, double amount)
        {
            var trader = await _tradeRepository.GetTraderById(traderId);
            if (trader == null)
            {
                return TradeConstant.TraderNotRegisteredError;
            }
            var finalAmount = amount;
            int oneLakh = 100000;
            if (amount > oneLakh)
            {
                var chargableAmount = amount - oneLakh;
                finalAmount = chargableAmount * 0.95 + oneLakh;
            }
            await _tradeRepository.AddFund(traderId, finalAmount);
            return TradeConstant.FundAddSuccess;
        }
    }
}
