using FoodPrices.Services.Exceptions;
using FoodPrices.Services.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace FoodPrices.Services.Services
{
    public class FoodIndexService : IFoodIndexService
    {
        private readonly HttpClient httpClient;
        private readonly ICurrencyService currencyService;
        private readonly ILogger<FoodIndexService> logger;

        public FoodIndexService(HttpClient httpClient, ICurrencyService currencyService, ILogger<FoodIndexService> logger)
        {
            this.httpClient = httpClient;
            this.currencyService = currencyService;
            this.logger = logger;
        }

        public async Task<IList<FoodIndex>> GetAll(string currencyCode)
        {
            var isValidCurrency = await this.currencyService.IsValid(currencyCode);

            if (!isValidCurrency)
            {
                //TODO: improve error handling and logging
                throw new Exception($"Currency {currencyCode} is not supported");
            }

            //TODO improve error handling and logging (I believe this throws an exception if not success response)
            var myItemResponse = await this.httpClient.GetFromJsonAsync<MyItemsResponse>("api/myitems/data");

            if (myItemResponse == null)
            {
                //TODO improve error handling and logging
                throw new Exception($"An error has occured");
            }

            var foodIndices = new List<FoodIndex>();
            foreach (var item in myItemResponse.Items)
            {
                var foodIndex = new FoodIndex(item.ComCode, item.Description);
                foreach (var marketdata in item.MarketData)
                {
                    try
                    {
                        var convertedAmount = await this.currencyService.Convert(currencyCode, marketdata.High, marketdata.QuoteDate);
                        foodIndex.Quotes.Add(new Quote()
                        {
                            HighPrice = convertedAmount,
                            Date = marketdata.QuoteDate
                        });
                    }
                    catch (CurrencyConversionException ex)
                    {
                        this.logger.LogError(ex,
                            "Error when converting marketDataItem {marketDataItem} for item {comCode} with currency {currencyCode}",
                            JsonSerializer.Serialize(marketdata),
                            item.ComCode,
                            currencyCode);
                    }
                }
                foodIndices.Add(foodIndex);
            }

            return foodIndices;
        }
    }
}
