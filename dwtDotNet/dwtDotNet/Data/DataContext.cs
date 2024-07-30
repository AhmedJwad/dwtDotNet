using dwtDotNet.Share.Entites;
using Microsoft.EntityFrameworkCore;


namespace dwtDotNet.Data
{
    public class DataContext: DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

    }
}
