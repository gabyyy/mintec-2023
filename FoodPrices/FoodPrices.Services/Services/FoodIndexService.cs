using FoodPrices.Services.Models;
using System.Net.Http.Json;

namespace FoodPrices.Services.Services
{
    public class FoodIndexService : IFoodIndexService
    {
        private readonly HttpClient httpClient;

        public FoodIndexService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<FoodIndex>> GetAll(string currencyCode)
        {
            //TODO error handling (I believe this throws an exception if not success response)
            var myItemResponse = await this.httpClient.GetFromJsonAsync<MyItemsResponse>("api/myitems/data");

            if (myItemResponse == null)
            {
                //TODO error handling
                throw new NotImplementedException();
            }

            foreach (var item in myItemResponse.Items)
            {
                //TODO: map, and convert currency
            }

            throw new NotImplementedException();
        }
    }
}
