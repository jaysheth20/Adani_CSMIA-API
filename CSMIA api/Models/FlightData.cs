using Newtonsoft.Json;

namespace CSMIA_api.Models
{
    public class FlightData
    {
        [JsonProperty("UID")]
        public long UID { get; set; }

        [JsonProperty("airline_code")]
        public string AirlineCode { get; set; }

        [JsonProperty("Flight_number")]
        public string FlightNumber { get; set; }

        [JsonProperty("StatusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }

        [JsonProperty("CITY_CODE")]
        public string CityCode { get; set; }

        [JsonProperty("Nature")]
        public string Nature { get; set; }

        [JsonProperty("Qualifier")]
        public string Qualifier { get; set; }

        [JsonProperty("SCHED_DATE")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? SchedDate { get; set; }

        [JsonProperty("EST_DATE")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? EstDate { get; set; }

        [JsonProperty("ACT_DATE")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? ActDate { get; set; }

        [JsonProperty("Terminal")]
        public string Terminal { get; set; }

        [JsonProperty("RegistrationNo")]
        public string RegistrationNo { get; set; }

        [JsonProperty("GATES")]
        public string Gates { get; set; }

        [JsonProperty("Counters")]
        public string Counters { get; set; }

        [JsonProperty("Carousels")]
        public string Carousels { get; set; }

        [JsonProperty("CODESHARE1")]
        public string CodeShare1 { get; set; }

        [JsonProperty("CODESHARE2")]
        public string CodeShare2 { get; set; }

        [JsonProperty("UPDATETIMESTAMP")]
        public DateTime? UpdatedTimeStmp { get; set; }

        [JsonProperty("Stand")]
        public string Stand { get; set; }
    }
}
