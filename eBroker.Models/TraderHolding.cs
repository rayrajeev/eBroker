using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace eBroker.Models
{
    [ExcludeFromCodeCoverage]
    public class TraderHolding
    {
        public int EquityId { get; set; }

        public int TraderId { get; set; }

        public int Units { get; set; }
    }
}
