using System.Diagnostics.CodeAnalysis;

namespace eBroker.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class TraderHolding
    {
        public int EquityId { get; set; }

        public int TraderId { get; set; }

        public int Units { get; set; }
    }
}
