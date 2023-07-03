namespace FoodPrices.Services.Models
{
    public class Quote
    {
        /// <summary>
        /// Date of the quote
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Highest price
        /// </summary>
        public decimal HighPrice { get; set; }
    }
}
