using System;

namespace eBroker.Helper
{
    public class TradeHelperWrapper : ITradeHelperWrapper
    {
        public bool IsValidTradeTime(DateTime? dateTime = null)
        {
            return TradeHelper.IsValidTradeTime(dateTime);
        }
    }
}
