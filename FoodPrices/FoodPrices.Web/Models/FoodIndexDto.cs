namespace FoodPrices.Web.Models
{
    /// <summary>
    /// Food price information
    /// </summary>
    public class FoodIndexDto
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <example>4915</example>
        public int Id { get; set; }

        /// <summary>
        /// Description of index
        /// </summary>
        /// <example>Urner Barry Beef Index</example>
        public string Description { get; set; }

        /// <summary>
        /// Collection of quotes
        /// </summary>
        public IList<QuoteDto> Quotes { get; set; }
    }
}
