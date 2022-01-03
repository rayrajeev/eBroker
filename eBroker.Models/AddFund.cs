using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace eBroker.Models
{
    [ExcludeFromCodeCoverage]
    public class AddFund
    {
        public int TraderId { get; set; }

        public double Funds { get; set; }
    }
}
