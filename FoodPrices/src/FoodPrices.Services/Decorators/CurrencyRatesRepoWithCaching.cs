using FoodPrices.Services.Models;
using FoodPrices.Services.Services;
using Microsoft.Extensions.Caching.Memory;

namespace FoodPrices.Services.Decorators
{
    public class CurrencyRatesRepoWithCaching : ICurrencyRatesRepo
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICurrencyRatesRepo currencyRatesRepo;

        public CurrencyRatesRepoWithCaching(IMemoryCache memoryCache, ICurrencyRatesRepo currencyRatesRepo)
        {
            this.memoryCache = memoryCache;
            this.currencyRatesRepo = currencyRatesRepo;
        }

        public async Task<IEnumerable<CurrencyRate>> GetAll()
        {
            var rates = this.memoryCache.Get<IEnumerable<CurrencyRate>>("currencyRates");

            if (rates == null)
            {
                rates = await this.currencyRatesRepo.GetAll();
                this.memoryCache.Set("currencyRates", rates);
            }

            return rates;
        }
    }
}
