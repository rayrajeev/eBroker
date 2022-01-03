using eBroker.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace eBroker.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class EBrokerContext : DbContext
    {
        public EBrokerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Equity> Equities { get; set; }

        public DbSet<Trader> Traders { get; set; }

        public DbSet<TraderHolding> TraderHoldings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TraderHolding>()
                .HasKey(mr => new { mr.TraderId, mr.EquityId });

            modelBuilder.Entity<Equity>()
                .HasData(
                        new Equity
                        {
                            Id = 1,
                            Name = "TCS",
                            UnitPrice = 400
                        },
                        new Equity
                        {
                            Id = 2,
                            Name = "SBI",
                            UnitPrice = 300
                        });

            modelBuilder.Entity<Trader>()
                .HasData(
                        new Trader
                        {
                            Id = 1,
                            Name = "Rajeev",
                            Funds = 5000
                        },
                        new Trader
                        {
                            Id = 2,
                            Name = "Alok",
                            Funds = 2000
                        });
        }
    }
}
