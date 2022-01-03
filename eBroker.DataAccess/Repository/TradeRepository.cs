using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eBroker.DataAccess.Entities;
using System.Linq;

namespace eBroker.DataAccess.Repository
{
    public class TradeRepository : ITradeRepository
    {
        private readonly EBrokerContext _ebrokerContext;
        public TradeRepository(EBrokerContext dbContext)
        {
            _ebrokerContext = dbContext;
        }

        public async Task<Equity> GetEquityByName(string equityName)
        {
            return await _ebrokerContext.Equities.FirstOrDefaultAsync(eq => eq.Name == equityName);
        }

        public async Task<Trader> GetTraderById(int traderId)
        {
            return await _ebrokerContext.Traders.Include(x => x.TradeHoldings).FirstOrDefaultAsync(t => t.Id == traderId);
        }
        public async Task<bool> Buy(int traderId, string equityName, int quantity)
        {
            var trader = _ebrokerContext.Traders.Include(x => x.TradeHoldings).FirstOrDefault(t => t.Id == traderId);
            var equity = _ebrokerContext.Equities.FirstOrDefault(eq => eq.Name == equityName);
            if(trader == null || equity == null)
            {
                return false;
            }
            var traderEquityHolding = trader.TradeHoldings.FirstOrDefault(eq => eq.EquityId == equity.Id);
            if(traderEquityHolding == null)
            {                
                traderEquityHolding = new TraderHolding()
                {
                    EquityId = equity.Id,
                    TraderId = trader.Id,
                    Units = 0
                };
                trader.TradeHoldings.Add(traderEquityHolding);
                
            }
            traderEquityHolding.Units += quantity;
            trader.Funds -= quantity * equity.UnitPrice;

            int rowaUpdated = await _ebrokerContext.SaveChangesAsync();

            return rowaUpdated > 0;
        }

        public async Task<bool> Sell(int traderId, string equityName, int quantity, double brokerage)
        {
            var trader = _ebrokerContext.Traders.Include(x => x.TradeHoldings).FirstOrDefault(t => t.Id == traderId);
            var equity = _ebrokerContext.Equities.FirstOrDefault(eq => eq.Name == equityName);
            if (trader == null || equity == null)
            {
                return false;
            }
            var traderEquityHoldings = trader.TradeHoldings.FirstOrDefault(eq => eq.EquityId == equity.Id);
            if(traderEquityHoldings == null || traderEquityHoldings.Units < quantity)
            {
                return false;
            }
            traderEquityHoldings.Units -= quantity;
            trader.Funds += quantity * equity.UnitPrice - brokerage;
            int rowaUpdated = await _ebrokerContext.SaveChangesAsync();

            return rowaUpdated > 0;
        }

        public async Task<bool> AddFund(int traderId, double amount)
        {
            var trader = _ebrokerContext.Traders.Include(x => x.TradeHoldings).FirstOrDefault(t => t.Id == traderId);
            if(trader == null)
            {
                return false;
            }
            trader.Funds += amount;
            int rowaUpdated = await _ebrokerContext.SaveChangesAsync();
            return rowaUpdated > 0;
        }
    }
}
