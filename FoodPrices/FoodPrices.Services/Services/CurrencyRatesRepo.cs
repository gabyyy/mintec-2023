using FoodPrices.Services.Models;
using FoodPrices.Services.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FoodPrices.Services.Services
{
    //TODO: Add caching decorator
    public class CurrencyRatesRepo : ICurrencyRatesRepo
    {
        private readonly CurrencyRatesOptions options;

        public CurrencyRatesRepo(IOptions<CurrencyRatesOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<IEnumerable<CurrencyRate>> GetAll()
        {
            var json = await File.ReadAllTextAsync(this.options.JsonFilePath);

            //TODO: improve error handling
            return JsonSerializer.Deserialize<IEnumerable<CurrencyRate>>(json);
        }
    }
}
