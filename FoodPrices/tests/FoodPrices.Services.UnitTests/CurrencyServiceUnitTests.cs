using FoodPrices.Services.Models;
using FoodPrices.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace FoodPrices.Services.UnitTests
{
    public class CurrencyServiceUnitTests
    {
        private Mock<ICurrencyRatesRepo> repo;
        private CurrencyService sut;

        public CurrencyServiceUnitTests()
        {
            var logger = new Mock<ILogger<CurrencyService>>();
            this.repo = new Mock<ICurrencyRatesRepo>();
            this.sut = new CurrencyService(this.repo.Object, logger.Object);
        }

        [Fact]
        public async Task Convert_When_RateAvailable_Should_Return_Correct_Amount()
        {
            var toCurrencyCode = "GBP";
            var amount = 1.3m;
            var baseDate = DateTime.Now;

            var rates = new List<CurrencyRate>()
            {
                new CurrencyRate()
                {
                    CurrencyCode = toCurrencyCode,
                    ExchangeDate = baseDate,
                    ExchangeRate = 1.3m,
                },
                new CurrencyRate()
                {
                    CurrencyCode = toCurrencyCode,
                    ExchangeDate = baseDate.AddDays(-1),
                    ExchangeRate = 1.4m,
                },
                new CurrencyRate()
                {
                    CurrencyCode = "EUR",
                    ExchangeDate = baseDate,
                    ExchangeRate = 1.2m,
                }
            };

            this.repo.Setup(x=>x.GetAll()).ReturnsAsync(rates);

            var result = await this.sut.Convert(toCurrencyCode, amount, baseDate);

            result.ShouldBe(1m);
        }

        [Fact]
        public async Task Convert_When_RateNotAvailable_Should_Return_ConvertedAmount_With_Closed_DateExchangeRate()
        {
            var toCurrencyCode = "GBP";
            var amount = 1.3m;
            var baseDate = DateTime.Now;

            var rates = new List<CurrencyRate>()
            {
                new CurrencyRate()
                {
                    CurrencyCode = toCurrencyCode,
                    ExchangeDate = baseDate.AddDays(-1),
                    ExchangeRate = 1,
                },
                new CurrencyRate()
                {
                    CurrencyCode = "EUR",
                    ExchangeDate = baseDate,
                    ExchangeRate = 1.2m,
                }
            };

            this.repo.Setup(x => x.GetAll()).ReturnsAsync(rates);

            var result = await this.sut.Convert(toCurrencyCode, amount, baseDate);

            result.ShouldBe(1.3m);
        }
    }
}