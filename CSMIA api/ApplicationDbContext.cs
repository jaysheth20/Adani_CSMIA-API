using CSMIA_api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMIA_api
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FlightData> ArrivDomCurrent { get; set; }
        public DbSet<FlightData> ArrivInterCurrent { get; set; }
        public DbSet<FlightData> CArrivDomCurrent { get; set; }
        public DbSet<FlightData> CArrivInterCurrent { get; set; }
        public DbSet<FlightData> CDepDomCurrent { get; set; }
        public DbSet<FlightData> CDepInterCurrent { get; set; }
        public DbSet<FlightData> DepDomCurrent { get; set; }
        public DbSet<FlightData> DepInterCurrent { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

    }
}
