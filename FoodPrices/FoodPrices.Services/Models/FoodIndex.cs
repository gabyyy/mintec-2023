namespace FoodPrices.Services.Models
{
    public class FoodIndex
    {
        public FoodIndex(int id, string description)
        {
            Id = id;
            Description = description;
            Quotes = new List<Quote>();
        }

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
        public IList<Quote> Quotes { get; set; }
        
    }
}