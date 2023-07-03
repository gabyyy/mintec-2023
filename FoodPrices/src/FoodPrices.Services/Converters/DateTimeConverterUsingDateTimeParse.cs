using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;

namespace FoodPrices.Services.Converters
{
    public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        private readonly string dateTimeFormat;

        public DateTimeConverterUsingDateTimeParse(string dateTimeFormat)
        {
            this.dateTimeFormat = dateTimeFormat;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //TODO: improve error handling here
            return DateTime.ParseExact(reader.GetString() ?? string.Empty, this.dateTimeFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
