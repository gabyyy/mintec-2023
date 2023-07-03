using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoodPrices.Services.Converters
{
    internal class CustomDecimalConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                return !reader.TryGetDecimal(out var result) ? default : result;
            }
            catch (Exception)
            {
                //TODO: add logging
                return default;
            }
            
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
