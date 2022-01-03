using Microsoft.EntityFrameworkCore;
using System;
using eBroker.DataAccess.Entities;

namespace eBroker.DataAccess.Test
{
    public class EBrokerDataFixture : IDisposable
    {
        private bool disposed = false;
        public EBrokerContext BrokerContext { get; private set; }

        public EBrokerDataFixture()
        {
            var options = new DbContextOptionsBuilder<EBrokerContext>()
            .UseInMemoryDatabase(databaseName: "EBrokerDB").Options;

            BrokerContext = new EBrokerContext(options);

            BrokerContext.Traders.Add(new Trader { Id = 1, Name = "Rajeev", Funds = 5000 });
            BrokerContext.Traders.Add(new Trader { Id = 2, Name = "Alok", Funds = 2000 });
            
            BrokerContext.Equities.Add(new Equity { Id = 1, Name = "TCS", UnitPrice = 200 });
            BrokerContext.Equities.Add(new Equity { Id = 2, Name = "SBI", UnitPrice = 300 });

            BrokerContext.TraderHoldings.Add(new TraderHolding { EquityId = 1, TraderId = 1, Units = 10 });
            BrokerContext.TraderHoldings.Add(new TraderHolding { EquityId = 2, TraderId = 2, Units = 20 });
                            
            BrokerContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    BrokerContext.Dispose();
                }
                disposed = true;
            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }
    }
}
