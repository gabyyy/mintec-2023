using FoodPrices.Services.Models;
using System.Net.Http.Json;

namespace FoodPrices.Services.Services
{
    public class FoodIndexService : IFoodIndexService
    {
        private readonly HttpClient httpClient;
        private readonly ICurrencyService currencyService;

        public FoodIndexService(HttpClient httpClient, ICurrencyService currencyService)
        {
            this.httpClient = httpClient;
            this.currencyService = currencyService;
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
                    //TODO: take currency code from items[].currency
                    var convertedAmount = await this.currencyService.Convert("USD", currencyCode, marketdata.High, marketdata.QuoteDate);
                    foodIndex.Quotes.Add(new Quote()
                    {
                        HighPrice = convertedAmount,
                        Date = marketdata.QuoteDate
                    });
                }
                foodIndices.Add(foodIndex);
            }

            return foodIndices;
        }
    }
}
