using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace eBroker.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class Equity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double UnitPrice { get; set; }
    }
}
