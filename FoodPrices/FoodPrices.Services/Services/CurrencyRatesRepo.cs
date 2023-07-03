using FoodPrices.Services.Converters;
using FoodPrices.Services.Models;
using FoodPrices.Services.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FoodPrices.Services.Services
{
    //TODO: Add caching decorator or lazy loading
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

            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverterUsingDateTimeParse(this.options.DateTimeFormat));
            options.Converters.Add(new CustomDecimalConverter());

            //TODO: improve error handling
            return JsonSerializer.Deserialize<IEnumerable<CurrencyRate>>(json, options);
        }
    }
}
