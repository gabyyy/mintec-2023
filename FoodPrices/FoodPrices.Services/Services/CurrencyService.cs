namespace FoodPrices.Services.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRatesRepo currencyRatesRepo;

        public CurrencyService(ICurrencyRatesRepo currencyRatesRepo)
        {
            this.currencyRatesRepo = currencyRatesRepo;
        }

        public async Task<decimal> Convert(string fromCurrencyCode, string toCurrencyCode, decimal amount, DateTimeOffset baseDate)
        {
            var rates = await this.currencyRatesRepo.GetAll();

            //TODO: improve logic as we might not always find exact date?
            var targetRate = rates.SingleOrDefault(x => x.CurrencyCode == toCurrencyCode && x.ExchangeDate == baseDate);

            if (targetRate == null)
            {
                //TODO: improve error handling and logging
                throw new Exception($"Rate not found for {toCurrencyCode} and {baseDate}");
            }

            return amount / targetRate.ExchangeRate;
        }

        public async Task<bool> IsValid(string currencyCode)
        {
            var rates = await this.currencyRatesRepo.GetAll();

            return rates.Any(c => c.CurrencyCode == currencyCode);
        }
    }
}
