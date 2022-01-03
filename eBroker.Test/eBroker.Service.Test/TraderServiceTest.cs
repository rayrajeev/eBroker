using eBroker.DataAccess.Entities;
using eBroker.DataAccess.Repository;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace eBroker.Service.Test
{
    /// <summary>
    /// System Under Test
    /// Trade Service
    /// </summary>
    public class TradeServiceTest
    {
        private readonly Mock<ITradeRepository> _mockTradeRepository;

        private readonly TradeService _tradeService;

        public TradeServiceTest()
        {
            _mockTradeRepository = new Mock<ITradeRepository>();
            _tradeService = new TradeService(_mockTradeRepository.Object);
        }


        [Fact]
        public async Task TradeService_BuyEquity_InvalidEquity()
        {
            //Arrange
            int traderId = 1; string equityName = "MRF"; int shareCount = 2;
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync((Equity)null);

            //Act
            var result = await _tradeService.BuyEquity(traderId, equityName, shareCount);

            //Assert
            Assert.Equal(TradeConstant.EquityNotListedError, result);
        }

        [Fact]
        public async Task TradeService_BuyEquity_TraderNotRegistered()
        {
            //Arrange
            int traderId = 1; string equityName = "MRF"; int shareCount = 2;
            var equity = new Equity { Id = traderId, Name = equityName, UnitPrice = 200 };
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync(equity);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync((Trader)null);

            //Act
            var result = await _tradeService.BuyEquity(traderId, equityName, shareCount);

            //Assert
            Assert.Equal(TradeConstant.TraderNotRegisteredError, result);
        }


        [Fact]
        public async Task TradeService_BuyEquity_InsufficientFunds()
        {
            //Arrange

            int traderId = 1; string equityName = "MRF"; int shareCount = 2;
            var equity = new Equity { Id = 1, Name = equityName, UnitPrice = 200 };
            var trader = new Trader { Id = traderId, Name = "Rajeev", Funds = 100, TradeHoldings = new List<TraderHolding>() };
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync(equity);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(trader);

            //Act
            var result = await _tradeService.BuyEquity(trader.Id, equity.Name, shareCount);

            //Assert
            Assert.Equal(TradeConstant.NotEnoughFundError, result);
        }

        [Fact]
        public async Task TradeService_BuyEquity_Success()
        {
            //Arrange

            int traderId = 1; string equityName = "MRF"; int shareCount = 2;
            var equity = new Equity { Id = 1, Name = equityName, UnitPrice = 200 };
            var trader = new Trader { Id = traderId, Name = "Rajeev", Funds = 1000, TradeHoldings = new List<TraderHolding>() };
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync(equity);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(trader);
            _mockTradeRepository.Setup(x => x.Buy(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);

            //Act
            var result = await _tradeService.BuyEquity(trader.Id, equity.Name, shareCount);

            //Assert
            Assert.Equal(TradeConstant.OrderSuccess, result);
        }

        [Fact]
        public async Task TradeService_SellEquity_InvalidEquity()
        {
            //Arrange
            int traderId = 1; string equityName = "MRF"; int shareCount = 2;
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync((Equity)null);

            //Act
            var result = await _tradeService.SellEquity(traderId, equityName, shareCount);

            //Assert
            Assert.Equal(TradeConstant.EquityNotListedError, result);
        }

        [Fact]
        public async Task TradeService_SellEquity_TraderNotRegistered()
        {
            //Arrange
            int traderId = 1; string equityName = "MRF"; int shareCount = 2;
            var equity = new Equity { Id = traderId, Name = equityName, UnitPrice = 200 };
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync(equity);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync((Trader)null);

            //Act
            var result = await _tradeService.SellEquity(traderId, equityName, shareCount);

            //Assert
            Assert.Equal(TradeConstant.TraderNotRegisteredError, result);
        }

        [Fact]
        public async Task TradeService_SellEquity_InsufficientHoldings()
        {
            //Arrange
            int traderId = 1; string equityName = "MRF"; int shareCount = 4;
            var equity = new Equity { Id = 1, Name = equityName, UnitPrice = 200 };
            var trader = new Trader
            {
                Id = traderId,
                Name = "Rajeev",
                Funds = 1000,
                TradeHoldings = new List<TraderHolding> {
                new TraderHolding {TraderId = traderId, EquityId = equity.Id, Units = 2 }
            }
            };
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync(equity);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(trader);

            //Act
            var result = await _tradeService.SellEquity(trader.Id, equity.Name, shareCount);

            //Assert
            Assert.Equal(TradeConstant.TradeHoldingError, result);
        }

        [Fact]
        public async Task TradeService_SellEquity_Success()
        {
            //Arrange
            int traderId = 1; string equityName = "MRF"; int shareCount = 4;
            var equity = new Equity { Id = 1, Name = equityName, UnitPrice = 200 };
            var trader = new Trader
            {
                Id = traderId,
                Name = "Rajeev",
                Funds = 1000,
                TradeHoldings = new List<TraderHolding> {
                new TraderHolding {TraderId = traderId, EquityId = equity.Id, Units = 20 }
            }
            };
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync(equity);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(trader);
            _mockTradeRepository.Setup(x => x.Sell(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(true);

            //Act
            var result = await _tradeService.SellEquity(trader.Id, equity.Name, shareCount);

            //Assert
            Assert.Equal(TradeConstant.OrderSuccess, result);
        }

        [Fact]
        public async Task TradeService_SellEquity_BrokerageCharged05Percent()
        {
            //Arrange
            int traderId = 1; string equityName = "MRF"; int shareCount = 4;
            var equity = new Equity { Id = 1, Name = equityName, UnitPrice = 200 };
            var trader = new Trader
            {
                Id = traderId,
                Name = "Rajeev",
                Funds = 1000,
                TradeHoldings = new List<TraderHolding> {
                new TraderHolding {TraderId = traderId, EquityId = equity.Id, Units = 20 }
            }
            };
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync(equity);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(trader);
            _mockTradeRepository.Setup(x => x.Sell(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(true);

            //Act
            var result = await _tradeService.SellEquity(trader.Id, equity.Name, shareCount);

            //Assert
            Assert.Equal(TradeConstant.OrderSuccess, result);
            _mockTradeRepository.VerifyAll();
        }

        [Fact]
        public async Task TradeService_SellEquity_BrokerageChargedMin20()
        {
            //Arrange
            int traderId = 1; string equityName = "MRF"; int shareCount = 4;
            var equity = new Equity { Id = 1, Name = equityName, UnitPrice = 200 };
            var trader = new Trader
            {
                Id = traderId,
                Name = "Rajeev",
                Funds = 1000,
                TradeHoldings = new List<TraderHolding> {
                new TraderHolding {TraderId = traderId, EquityId = equity.Id, Units = 20 }
            }
            };
            _mockTradeRepository.Setup(x => x.GetEquityByName(It.IsAny<string>())).ReturnsAsync(equity);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(trader);
            _mockTradeRepository.Setup(x => x.Sell(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(true);

            //Act
            var result = await _tradeService.SellEquity(trader.Id, equity.Name, shareCount);

            //Assert
            Assert.Equal(TradeConstant.OrderSuccess, result);
            _mockTradeRepository.VerifyAll();
        }

        [Fact]
        public async Task TradeService_AddFund_InvalidTrader()
        {
            //Arrange
            int traderId = 1; int fund = 1000;
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync((Trader)null);

            //Act
            var result = await _tradeService.AddFunds(traderId, fund);

            //Assert
            Assert.Equal(TradeConstant.TraderNotRegisteredError, result);
        }

        [Fact]
        public async Task TradeService_AddFund_Success()
        {
            //Arrange
            int traderId = 1; int fund = 1000;
            var trader = new Trader
            {
                Id = traderId,
                Name = "Rajeev",
                Funds = 1000,
                TradeHoldings = new List<TraderHolding>()
            };
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(trader);

            //Act
            var result = await _tradeService.AddFunds(traderId, fund);

            //Assert
            Assert.Equal(TradeConstant.FundAddSuccess, result);
            _mockTradeRepository.VerifyAll();
        }        
    }
}
