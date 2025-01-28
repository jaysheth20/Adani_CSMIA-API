namespace CSMIA_api.Models
{
    public class FlightDataModel
    {
        public long UID { get; set; }
        public string Airline_Code { get; set; }
        public string Flight_number { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string City_Name { get; set; }
        public string CITY_CODE { get; set; }
        public string Nature { get; set; }
        public string Qualifier { get; set; }
        public DateTime? SCHED_DATE { get; set; }
        public DateTime? EST_DATE { get; set; }
        public DateTime? ACT_DATE { get; set; }
        public string Terminal { get; set; }
        public string RegistrationNo { get; set; }
        public string GATES { get; set; }
        public string COUNTERS { get; set; }
        public string CAROUSELS { get; set; }
        public string CODESHARE1 { get; set; }
        public string CODESHARE2 { get; set; }
        public DateTime? UPDATETIMESTAMP { get; set; }
        public string Stand { get; set; }
    }
}
