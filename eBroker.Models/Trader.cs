using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace eBroker.Models
{
    [ExcludeFromCodeCoverage]
    public class Trader
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public double Funds { get; set; }

        public IEnumerable<TraderHolding> TradeHoldings { get; set; }
    }
}
