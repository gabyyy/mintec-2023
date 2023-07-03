namespace FoodPrices.Services.Models
{
    public class CurrencyRate
    {
        public string CurrencyCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public DateTimeOffset ExchangeDate { get; set; }
    }
}
