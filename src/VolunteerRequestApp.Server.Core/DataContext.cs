using Microsoft.EntityFrameworkCore;

namespace VolunteerRequestApp.Server.Core
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(new List<Status> {
                new Status {Id = 1, Title = "Чорновик" },
                new Status {Id = 2, Title = "Активний" },
                new Status {Id = 3, Title = "Завершений" },
                new Status {Id = 4, Title = "Відмінений" },
                new Status {Id = 5, Title = "Архівний" }
            });

            modelBuilder.Entity<Request>().HasMany(x=> x.Tags).WithMany(x => x.Requests);

            base.OnModelCreating(modelBuilder);
        }
    }
}