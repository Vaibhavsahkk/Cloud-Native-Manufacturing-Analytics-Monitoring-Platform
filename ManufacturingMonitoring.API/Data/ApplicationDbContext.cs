using Microsoft.EntityFrameworkCore;
using ManufacturingMonitoring.API.Models;

namespace ManufacturingMonitoring.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AlertConfiguration> AlertConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired();
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.CreatedAt).IsRequired();
            });

            // Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired();
            });

            // AlertConfiguration entity
            modelBuilder.Entity<AlertConfiguration>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.MetricType).IsRequired();
                entity.Property(a => a.Severity).IsRequired();
                entity.Property(a => a.CreatedAt).IsRequired();
            });
        }
    }
}
