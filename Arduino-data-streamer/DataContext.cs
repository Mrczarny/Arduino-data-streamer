using Microsoft.EntityFrameworkCore;

namespace Arduino_data_streamer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<DataModel> DataModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataModel>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<DataModel>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);
        }
    }
}
