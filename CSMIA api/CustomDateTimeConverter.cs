using System;
using System.Globalization;
using Newtonsoft.Json;


namespace CSMIA_api
{

    public class CustomDateTimeConverter : JsonConverter
    {
        private readonly string[] _formats = { "dd/MM/yyyy HH:mm", "dd/MM/yyyy HH:mm:ss" };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var dateString = reader.Value.ToString();
            return DateTime.ParseExact(dateString, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
    }
}
