namespace CSMIA_api.Models
{
    public class FlightData
    {
        public long UID { get; set; }
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string Nature { get; set; }
        public string Qualifier { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? EstimatedDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public string Terminal { get; set; }
        public string RegistrationNo { get; set; }
        public string Gates { get; set; }
        public string Counters { get; set; }
        public string Carousels { get; set; }
        public string CodeShare1 { get; set; }
        public string CodeShare2 { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public string Stand { get; set; }
    }
}
