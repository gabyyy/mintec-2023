using FoodPrices.Services.Models;

namespace FoodPrices.Services.Services
{
    public interface IFoodIndexService
    {
        Task<IEnumerable<FoodIndex>> GetAll(string currency);
    }
}
