using System.Threading.Tasks;
using eBroker.Models;

namespace eBroker.Service
{
    public interface ITradeService
    {
        Task<string> BuyEquity(int traderId, string equiryName, int quantity);

        Task<string> AddFunds(int traderId, double amount);

        Task<string> SellEquity(int traderId, string equiryName, int quantity);
    }
}
