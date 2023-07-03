namespace FoodPrices.Services.Services
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Validate if currency code is valid/supported
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        Task<bool> IsValid(string currencyCode);

        /// <summary>
        /// Convert amount from one currency to another, based on historical rates
        /// </summary>
        /// <param name="fromCurrencyCode"></param>
        /// <param name="toCurrencyCode"></param>
        /// <param name="amount"></param>
        /// <param name="baseDate"></param>
        /// <returns></returns>
        Task<decimal> Convert(string fromCurrencyCode, string toCurrencyCode, decimal amount, DateTimeOffset baseDate);
    }
}
