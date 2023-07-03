namespace FoodPrices.Web.Models
{
    public class QuoteDto
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
