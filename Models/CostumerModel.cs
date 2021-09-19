using Microsoft.EntityFrameworkCore;

namespace eaudproj_react_design.Models
{
    public class CostumerContext : DbContext
    {
        public CostumerContext(DbContextOptions<CostumerContext> options)
            : base(options)
        {
        }

        public DbSet<Costumer> CostumerItems { get; set; }
    }
}