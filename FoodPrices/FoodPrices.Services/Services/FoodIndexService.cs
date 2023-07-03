using FoodPrices.Services.Models;

namespace FoodPrices.Services.Services
{
    public class FoodIndexService : IFoodIndexService
    {
        private readonly HttpClient httpClient;

        public FoodIndexService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<IEnumerable<FoodIndex>> GetAll()
        {
            var indices = new FoodIndex[]
            {
                new(4915, "Urner Barry Beef Index", new DateTimeOffset(new DateTime(2023, 6, 30), new TimeSpan()), 3.1488167999999996m)
            };
            return Task.FromResult(indices.AsEnumerable());
        }
    }
}
