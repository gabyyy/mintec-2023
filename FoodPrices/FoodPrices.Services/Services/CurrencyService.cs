using FoodPrices.Services.Exceptions;
using Microsoft.Extensions.Logging;

namespace FoodPrices.Services.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRatesRepo currencyRatesRepo;
        private readonly ILogger<CurrencyService> logger;

        public CurrencyService(ICurrencyRatesRepo currencyRatesRepo, ILogger<CurrencyService> logger)
        {
            this.currencyRatesRepo = currencyRatesRepo;
            this.logger = logger;
        }

        public async Task<decimal> Convert(string toCurrencyCode, decimal amount, DateTime baseDate)
        {
            try
            {
                var rates = await this.currencyRatesRepo.GetAll();

                //TODO: improve logic as we might not always find exact date?
                var targetRate = rates.SingleOrDefault(x => x.CurrencyCode == toCurrencyCode && x.ExchangeDate == baseDate);

                if (targetRate == null)
                {
                    this.logger.LogWarning("Exchange rate not found for {toCurrencyCode} and date {basedate}. Using latest currency rate",
                        toCurrencyCode,
                        baseDate);
                    targetRate = rates.Where(x => x.CurrencyCode == toCurrencyCode).MaxBy(x => x.ExchangeDate);
                }

                if (targetRate.ExchangeRate == 0)
                {
                    //TODO: improve error handling and logging
                    throw new CurrencyConversionException($"Rate not found for {toCurrencyCode} and {baseDate}");
                }

                return amount / targetRate.ExchangeRate;
            }
            catch (Exception e)
            {
                throw new CurrencyConversionException("An error occured while during currency conversion");
            }            
        }

        public async Task<bool> IsValid(string currencyCode)
        {
            var rates = await this.currencyRatesRepo.GetAll();

            return rates.Any(c => c.CurrencyCode == currencyCode);
        }
    }
}
