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
                //TODO: error handling
                throw new NotImplementedException();
            }

            //TODO error handling (I believe this throws an exception if not success response)
            var myItemResponse = await this.httpClient.GetFromJsonAsync<MyItemsResponse>("api/myitems/data");

            if (myItemResponse == null)
            {
                //TODO error handling
                throw new NotImplementedException();
            }

            var foodIndices = new List<FoodIndex>();
            foreach (var item in myItemResponse.Items)
            {
                var foodIndex = new FoodIndex(item.ComCode, item.Description);
                foreach (var marketdata in item.MarketData)
                {
                    var convertedAmount = await this.currencyService.Convert("GBP", currencyCode, marketdata.High, marketdata.QuoteDate);
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
