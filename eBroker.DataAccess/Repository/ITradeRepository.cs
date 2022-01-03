using eBroker.DataAccess.Entities;
using System.Threading.Tasks;

namespace eBroker.DataAccess.Repository
{
    public interface ITradeRepository
    {
        Task<Equity> GetEquityByName(string equityName);

        Task<Trader> GetTraderById(int traderId);

        Task<bool> Buy(int traderId, string equityName, int quantity);

        Task<bool> Sell(int traderId, string equityName, int quantity, double brokerage);

        Task<bool> AddFund(int traderId, double amount);
    }
}
