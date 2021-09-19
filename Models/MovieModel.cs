using Microsoft.EntityFrameworkCore;

namespace eaudproj_react_design.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> MovieItems { get; set; }
    }
}