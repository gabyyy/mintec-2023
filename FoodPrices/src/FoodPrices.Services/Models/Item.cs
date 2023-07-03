namespace FoodPrices.Services.Models
{
    public class Item
    {
        public int ComCode { get; set; }

        public string Description { get; set; }
        
        public IEnumerable<Marketdata> MarketData { get; set; }
    }

}
