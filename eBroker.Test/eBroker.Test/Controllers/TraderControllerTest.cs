using eBroker.Controllers;
using eBroker.Helper;
using eBroker.Models;
using eBroker.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace eBroker.Test.Controllers
{
    public class TraderControllerTest
    {
        private readonly Mock<ITradeService> _mockTradeService;
        private readonly TraderController traderController;
        private readonly Mock<ITradeHelperWrapper> _mockTradeHelperWrapper;

        public TraderControllerTest()
        {
            _mockTradeService = new Mock<ITradeService>();
            _mockTradeHelperWrapper = new Mock<ITradeHelperWrapper>();
            traderController = new TraderController(_mockTradeService.Object, _mockTradeHelperWrapper.Object);
        }

        [Theory]
        [MemberData(nameof(TradingDataSamples))]
        public async Task TraderController_Buy_BadRequest(TradingData tradingData, int expectedStatus, string expectedError)
        {
            //Arrange 
            //Act
            var result = await traderController.Buy(tradingData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(expectedStatus, result.StatusCode);
            Assert.Equal(expectedError, result.Value);
        }

        [Fact]
        public async Task TraderController_Buy_InvalidTrader()
        {
            //Arrange
            var tradingData = new TradingData { EquityName = "SBI", TraderId = 10, Quantity = 3 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTradeTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.BuyEquity(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(TradeConstant.TraderNotRegisteredError);

            //Act
            var result = await traderController.Buy(tradingData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(TradeConstant.TraderNotRegisteredError, result.Value);
        }

        [Fact]
        public async Task TraderController_Buy_InvalidEquity()
        {
            //Arrange
            var tradingData = new TradingData { EquityName = "SBI", TraderId = 10, Quantity = 3 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTradeTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.BuyEquity(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(TradeConstant.EquityNotListedError);

            //Act
            var result = await traderController.Buy(tradingData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(TradeConstant.EquityNotListedError, result.Value);
        }

        [Fact]
        public async Task TraderController_Buy_OutsideTradingWindow()
        {
            //Arrange
            var tradingData = new TradingData { EquityName = "SBI", TraderId = 10, Quantity = 3 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTradeTime(null)).Returns(false);
            _mockTradeService.Setup(s => s.BuyEquity(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(TradeConstant.TradeTime);

            //Act
            var result = await traderController.Buy(tradingData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(TradeConstant.TradeTime, result.Value);
        }

        [Fact]
        public async Task TraderController_Buy_Success()
        {
            //Arrange
            var tradingData = new TradingData { EquityName = "SBI", TraderId = 10, Quantity = 3 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTradeTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.BuyEquity(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(TradeConstant.OrderSuccess);

            //Act
            var result = await traderController.Buy(tradingData) as OkObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(TradeConstant.OrderSuccess, result.Value);
        }

        [Theory]
        [MemberData(nameof(TradingDataSamples))]
        public async Task TraderController_Sell_BadRequest(TradingData tradingData, int expectedStatus, string expectedError)
        {
            //Arrange 
            //Act
            var result = await traderController.Sell(tradingData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(expectedStatus, result.StatusCode);
            Assert.Equal(expectedError, result.Value);
        }

        [Fact]
        public async Task TraderController_Sell_InvalidTrader()
        {
            //Arrange
            var tradingData = new TradingData { EquityName = "SBI", TraderId = 10, Quantity = 3 };            
            _mockTradeHelperWrapper.Setup(s => s.IsValidTradeTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.SellEquity(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(TradeConstant.TraderNotRegisteredError);

            //Act
            var result = await traderController.Sell(tradingData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(TradeConstant.TraderNotRegisteredError, result.Value);
        }

        [Fact]
        public async Task TraderController_Sell_InvalidEquity()
        {
            //Arrange
            var tradingData = new TradingData { EquityName = "SBI", TraderId = 10, Quantity = 3 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTradeTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.SellEquity(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(TradeConstant.EquityNotListedError);

            //Act
            var result = await traderController.Sell(tradingData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(TradeConstant.EquityNotListedError, result.Value);
        }

        [Fact]
        public async Task TraderController_Sell_OutsideTradingWindow()
        {
            //Arrange
            var tradingData = new TradingData { EquityName = "SBI", TraderId = 10, Quantity = 3 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTradeTime(null)).Returns(false);
            _mockTradeService.Setup(s => s.SellEquity(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(TradeConstant.TradeTime);

            //Act
            var result = await traderController.Sell(tradingData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(TradeConstant.TradeTime, result.Value);
        }

        [Fact]
        public async Task TraderController_Sell_Success()
        {
            //Arrange
            var tradingData = new TradingData { EquityName = "SBI", TraderId = 10, Quantity = 3 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTradeTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.SellEquity(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(TradeConstant.OrderSuccess);

            //Act
            var result = await traderController.Sell(tradingData) as OkObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(TradeConstant.OrderSuccess, result.Value);
        }

        [Theory]
        [MemberData(nameof(TradeFundSampleData))]
        public async Task TraderController_AddFund_InValidRequest(AddFund tradeFundRequest, int expectedStatus, string expectedError)
        {
            //Act
            var result = await traderController.AddFund(tradeFundRequest) as BadRequestObjectResult;
            //Assert
            Assert.Equal(expectedStatus, result.StatusCode);
            Assert.Equal(expectedError, result.Value);
        }


        [Fact]
        public async Task TraderController_AddFund_InValidTrader()
        {
            //Arrange
            var fundData = new AddFund() { TraderId = 2 , Funds = 1000, };
            _mockTradeService.Setup(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(TradeConstant.TraderNotRegisteredError);
            //Act
            var result = await traderController.AddFund(fundData) as BadRequestObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(TradeConstant.TraderNotRegisteredError, result.Value);
        }

        [Fact]
        public async Task TraderController_AddFund_Success()
        {
            //Arrange
            var fundData = new AddFund() { TraderId = 2, Funds = 1000, };
            _mockTradeService.Setup(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(TradeConstant.FundAddSuccess);
            //Act
            var result = await traderController.AddFund(fundData) as OkObjectResult;
            //Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(TradeConstant.FundAddSuccess, result.Value);
        }        

        public static IEnumerable<object[]> TradingDataSamples
        {
            get
            {
                return new List<object[]>
                                {
                new object[] {new TradingData { EquityName = "SBI" , TraderId = 0, Quantity =3 }, StatusCodes.Status400BadRequest, TradeConstant.InvalidTradeData },
                new object[] {new TradingData { EquityName = "SBI", TraderId = 1, Quantity = 0 }, StatusCodes.Status400BadRequest, TradeConstant.InvalidTradeData },
                new object[] {new TradingData { EquityName = "", TraderId = 1, Quantity = 3 }, StatusCodes.Status400BadRequest, TradeConstant.InvalidTradeData }
                };
            }
        }

        public static IEnumerable<object[]> TradeFundSampleData
        {
            get
            {
                return new List<object[]>
                                {
                new object[] {new AddFund {  TraderId = 0 ,Funds = 1100}, StatusCodes.Status400BadRequest, TradeConstant.TraderOrFundNotValid },
                new object[] {new AddFund { TraderId = 1 , Funds = 0 }, StatusCodes.Status400BadRequest, TradeConstant.TraderOrFundNotValid },
                };
            }
        }
    }
}
