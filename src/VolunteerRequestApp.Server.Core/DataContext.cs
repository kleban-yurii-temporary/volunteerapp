using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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
        public DbSet<CurrencyPair> CurrencyPairs { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(new List<Status> {
                new Status {Id = 1, Title = "Чорновик" },
                new Status {Id = 2, Title = "Активний" },
                new Status {Id = 3, Title = "Завершений" },
                new Status {Id = 4, Title = "Відмінений" },
                new Status {Id = 5, Title = "Архівний" }
            });

            modelBuilder.Entity<CurrencyPair>().HasData(new CurrencyPair[]
            {
                new CurrencyPair { Id = 1, СurrencyFrom = "UAH", СurrencyTo = "USD", IsActive = true},
                new CurrencyPair { Id = 2, СurrencyFrom = "UAH", СurrencyTo = "EUR", IsActive = true},
            });

            modelBuilder.Entity<ExchangeRate>().HasData(new ExchangeRate[]
            {
                new ExchangeRate { Id = 1, CurrencyPairId = 1, CreatedOn = DateTime.UtcNow, Value = 0.027076188 },
                new ExchangeRate { Id = 2, CurrencyPairId = 2, CreatedOn = DateTime.UtcNow, Value = 0.026223587 }
            });

            modelBuilder.Entity<Request>().HasMany(x=> x.Tags).WithMany(x => x.Requests);

            base.OnModelCreating(modelBuilder);
        }
    }
}