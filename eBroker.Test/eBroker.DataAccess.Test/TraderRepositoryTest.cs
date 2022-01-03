using eBroker.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace eBroker.DataAccess.Test
{

    public class TradeRepositoryTest : IClassFixture<EBrokerDataFixture>
    {
        private EBrokerContext _eBrokerContext;
        private TradeRepository _tradeRepository;
        public TradeRepositoryTest(EBrokerDataFixture fixture)
        {
            _eBrokerContext = fixture.BrokerContext;
            _tradeRepository = new TradeRepository(_eBrokerContext);
        }

        [Fact]
        public async Task TradeRepository_GetEquityByName_EquityNotExists()
        {
            //Arrange
            string equityName = "MRF";
            //Act
            var result = await _tradeRepository.GetEquityByName(equityName);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TradeRepository_GetEquityByName_EquityExists()
        {
            //Arrange
            string equityName = "SBI";
            //Act
            var result = await _tradeRepository.GetEquityByName(equityName);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TradeRepository_GetTraderById_NotExist()
        {
            //Arrange
            int traderId = 10;
            //Act
            var result = await _tradeRepository.GetTraderById(traderId);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TradeRepository_GetTraderById_Exist()
        {
            //Arrange
            int traderId = 1;
            //Act
            var result = await _tradeRepository.GetTraderById(traderId);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TradeRepository_Buy_Success()
        {
            //Arrange
            int traderId = 1;
            string equityName = "SBI";
            int shareCount = 2;
            //Act
           var tradeResult = await _tradeRepository.Buy(traderId, equityName, shareCount);

            //Assert
            Assert.True(tradeResult);
        }

        [Fact]
        public async Task TradeRepository_Buy_Fail()
        {
            //Arrange
            int traderId = 10;
            string equityName = "MRF";
            int shareCount = 2;
            //Act
            var tradeResult = await _tradeRepository.Buy(traderId, equityName, shareCount);

            //Assert
            Assert.False(tradeResult);
        }

        [Fact]
        public async Task TradeRepository_Sell_Success()
        {
            //Arrange
            int traderId = 1;
            string equityName = "SBI";
            int shareCount = 2;
            await _tradeRepository.Buy(traderId, equityName, shareCount);
            //Act
            var tradeResult = await _tradeRepository.Sell(traderId, equityName, shareCount,20);

            //Assert
            Assert.True(tradeResult);
        }

        [Fact]
        public async Task TradeRepository_Sell_Fail()
        {
            //Arrange
            int traderId = 10;
            string equityName = "MRF";
            int shareCount = 2;
            //Act
            var tradeResult = await _tradeRepository.Sell(traderId, equityName, shareCount,20);

            //Assert
            Assert.False(tradeResult);
        }

        [Fact]
        public async Task TradeRepository_AddFund_Success()
        {
            //Arrange
            int traderId = 1;
            int fund = 1000;           
            //Act
            var tradeResult = await _tradeRepository.AddFund(traderId,fund);

            //Assert
            Assert.True(tradeResult);
        }

        [Fact]
        public async Task TradeRepository_AddFund_Fail()
        {
            //Arrange
            int traderId = 10;
            int fund = 1000;
            //Act
            var tradeResult = await _tradeRepository.AddFund(traderId, fund);

            //Assert
            Assert.False(tradeResult);
        }

    }
}
