using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Service
{
    public class TradeConstant
    {
        public const string EquityNotListedError = "Error: Equity Not Listed";

        public const string TraderNotRegisteredError = "Error: Trader not registered";

        public const string NotEnoughFundError = "Error: Oops..you don't have enough fund";

        public const string TradeHoldingError = "Error: Can't sell,please check your holdings";

        public const string OrderSuccess = "Congratulation, your order is placed successfully";

        public const string FundAddSuccess = "Fund Added successFully";

        public const string InvalidTradeData = "Invalid Trade Data";

        public const string TradeTime = "Trading time is Monday to Friday between 9 AM to 3 PM";

        public const string TraderOrFundNotValid = "Trader or Fund is not valid";
    }
}
