using Domain.Entities;
using Infrastructure.Map;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Resume> Resume { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ResumeMap());
            modelBuilder.ApplyConfiguration(new EventMap());
            modelBuilder.ApplyConfiguration(new PatientMap());
            modelBuilder.ApplyConfiguration(new HospitalMap());
        }
    }
}
