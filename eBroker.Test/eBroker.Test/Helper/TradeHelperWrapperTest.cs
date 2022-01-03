using eBroker.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Test.Helper
{
    public class TradeHelperWrapperTest
    {
        private readonly TradeHelperWrapper _tradeHelperWrapper;
        public TradeHelperWrapperTest()
        {
            _tradeHelperWrapper = new TradeHelperWrapper();
        }
        [Fact]
        public void TradeHelperWrapper_IsValidTradeTime_True()
        {
            DateTime dateTime = new DateTime(2022, 01, 04, 12, 10, 10);
            var result = _tradeHelperWrapper.IsValidTradeTime(dateTime);
            Assert.True(result);
        }

        [Fact]
        public void TradeHelperWrapper_IsValidTradeTime_False()
        {
            DateTime dateTime = new DateTime(2022, 01, 01, 12, 20, 10);
            var result = _tradeHelperWrapper.IsValidTradeTime(dateTime);
            Assert.False(result);
        }
    }
}
