using eBroker.Helper;
using System;
using System.Collections.Generic;
using Xunit;

namespace eBroker.Test.Helper
{
    public class TradeHelperTest
    {
        [Theory, MemberData(nameof(TradeTimeSamples))]
        public void TradeHelper_IsTradeTimeValid(int year, int month, int day, int hour, int min, int sec, bool isValid)
        {
            DateTime dateTime = new DateTime(year, month, day, hour, min, sec);
            var result = TradeHelper.IsValidTradeTime(dateTime);
            Assert.Equal(result, isValid);
        }

        public static IEnumerable<object[]> TradeTimeSamples =>
        new List<object[]>
        {
            new object[] { 2022, 01, 01, 12, 0, 0, false  },
            new object[] { 2022, 01, 02, 10, 20, 30, false  },
            new object[] { 2022, 01, 03, 16, 10, 10, false  },
            new object[] { 2022, 01, 04, 12, 05, 10, true  },
            new object[] { 2022, 01, 05, 15, 0, 0, true  }
        };

    }
}
