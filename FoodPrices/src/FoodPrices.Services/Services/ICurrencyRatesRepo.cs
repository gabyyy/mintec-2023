using FoodPrices.Services.Models;

namespace FoodPrices.Services.Services
{
    public interface ICurrencyRatesRepo
    {
        Task<IEnumerable<CurrencyRate>> GetAll();
    }
}
