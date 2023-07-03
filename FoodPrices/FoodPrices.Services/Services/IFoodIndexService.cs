using FoodPrices.Services.Models;

namespace FoodPrices.Services.Services
{
    public interface IFoodIndexService
    {
        Task<IList<FoodIndex>> GetAll(string currency);
    }
}
