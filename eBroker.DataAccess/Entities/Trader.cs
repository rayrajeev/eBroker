using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace eBroker.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class Trader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Funds { get; set; }

        public IList<TraderHolding> TradeHoldings { get; set; }
    }
}
