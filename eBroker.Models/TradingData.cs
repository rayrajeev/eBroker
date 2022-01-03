using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace eBroker.Models
{
    [ExcludeFromCodeCoverage]
    public class TradingData
    {
        public int TraderId { get; set; }
        public string EquityName { get; set; }
        public int Quantity { get; set; }
    }
}
