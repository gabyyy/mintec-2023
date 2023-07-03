namespace FoodPrices.Web.Models
{
    public class FoodIndexDto
    {
        public FoodIndexDto(string id, string description, DateTimeOffset quoteDate, decimal highPrice)
        {
            Id = id;
            Description = description;
            QuoteDate = quoteDate;
            HighPrice = highPrice;
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <example>4915</example>
        public string Id { get; set; }

        /// <summary>
        /// Description of index
        /// </summary>
        /// <example>Urner Barry Beef Index</example>
        public string Description { get; set; }        

        /// <summary>
        /// The date of the quote
        /// </summary>
        public DateTimeOffset QuoteDate { get; set; }

        /// <summary>
        /// Highest price
        /// </summary>
        public decimal HighPrice { get; set; }
    }
}
