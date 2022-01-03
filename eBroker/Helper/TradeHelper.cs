using System;

namespace eBroker.Helper
{
    public static class TradeHelper
    {
        public static bool IsValidTradeTime(DateTime? inputDateTime = null)
        {
            var tradeTime = inputDateTime == null ? DateTime.Now : inputDateTime.Value;

            var trdeStartTime = new DateTime(tradeTime.Year, tradeTime.Month, tradeTime.Day, 9, 0, 0);
            var trdeEndTime = new DateTime(tradeTime.Year, tradeTime.Month, tradeTime.Day, 15, 0, 0);

            if (tradeTime.DayOfWeek == DayOfWeek.Saturday || tradeTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            return DateTime.Compare(tradeTime, trdeStartTime) >= 0 && DateTime.Compare(tradeTime, trdeEndTime) <= 0;
        }
       
    }
}
