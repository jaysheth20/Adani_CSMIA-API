using CSMIA_api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMIA_api
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FlightDataModel> ArrivDomCurrent { get; set; }
        public DbSet<FlightDataModel> ArrivInterCurrent { get; set; }
        public DbSet<FlightDataModel> CArrivDomCurrent { get; set; }
        public DbSet<FlightDataModel> CArrivInterCurrent { get; set; }
        public DbSet<FlightDataModel> CDepDomCurrent { get; set; }
        public DbSet<FlightDataModel> CDepInterCurrent { get; set; }
        public DbSet<FlightDataModel> DepDomCurrent { get; set; }
        public DbSet<FlightDataModel> DepInterCurrent { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

    }
}
