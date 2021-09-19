using Microsoft.EntityFrameworkCore;

namespace eaudproj_react_design.Models
{
    public class LocationContext : DbContext
    {
        public LocationContext(DbContextOptions<LocationContext> options)
            : base(options)
        {
        }

        public DbSet<Location> LocationItems { get; set; }
    }
}