using System.Text.Json.Serialization;

namespace CSMIA_api.Models
{
    public class FlightData
    {
        [JsonPropertyName("UID")]
        public long UID { get; set; }

        [JsonPropertyName("airline_code")]
        public string AirlineCode { get; set; }

        [JsonPropertyName("Flight_number")]
        public string FlightNumber { get; set; }

        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("city_name")]
        public string CityName { get; set; }

        [JsonPropertyName("CITY_CODE")]
        public string CityCode { get; set; }

        [JsonPropertyName("Nature")]
        public string Nature { get; set; }

        [JsonPropertyName("Qualifier")]
        public string Qualifier { get; set; }

        [JsonPropertyName("SCHED_DATE")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? SchedDate { get; set; }

        [JsonPropertyName("EST_DATE")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? EstDate { get; set; }

        [JsonPropertyName("ACT_DATE")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? ActDate { get; set; }

        [JsonPropertyName("Terminal")]
        public string Terminal { get; set; }

        [JsonPropertyName("RegistrationNo")]
        public string RegistrationNo { get; set; }

        [JsonPropertyName("GATES")]
        public string Gates { get; set; }

        [JsonPropertyName("Counters")]
        public string Counters { get; set; }

        [JsonPropertyName("Carousels")]
        public string Carousels { get; set; }

        [JsonPropertyName("CODESHARE1")]
        public string CodeShare1 { get; set; }

        [JsonPropertyName("CODESHARE2")]
        public string CodeShare2 { get; set; }

        [JsonPropertyName("UPDATETIMESTAMP")]
        public DateTime? UpdatedTimeStmp { get; set; }

        [JsonPropertyName("Stand")]
        public string Stand { get; set; }
    }
}
