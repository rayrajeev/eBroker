using System;

namespace eBroker.Helper
{
    public interface ITradeHelperWrapper
    {
        bool IsValidTradeTime(DateTime? dateTime = null);
    }
}
