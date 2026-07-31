using ManufacturingMonitoring.API.Models;

namespace ManufacturingMonitoring.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if roles already exist
            if (context.Roles.Any())
            {
                return; // DB has been seeded
            }

            // Seed Roles
            var roles = new Role[]
            {
                new Role { Name = "Admin" },
                new Role { Name = "Engineer" },
                new Role { Name = "Viewer" }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Seed Alert Configurations
            var alertConfigs = new AlertConfiguration[]
            {
                new AlertConfiguration
                {
                    MetricType = "CPU",
                    ThresholdValue = 85.0,
                    Severity = "High",
                    CreatedAt = DateTime.UtcNow
                },
                new AlertConfiguration
                {
                    MetricType = "Memory",
                    ThresholdValue = 90.0,
                    Severity = "High",
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.AlertConfigurations.AddRange(alertConfigs);
            context.SaveChanges();

            // Seed initial users
            var users = new User[]
            {
                new User
                {
                    Name = "Admin User",
                    Email = "admin@manufacturing.com",
                    RoleId = 1, // Admin
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Name = "Engineer User",
                    Email = "engineer@manufacturing.com",
                    RoleId = 2, // Engineer
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
