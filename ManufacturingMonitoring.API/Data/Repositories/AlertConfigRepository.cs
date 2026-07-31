using ManufacturingMonitoring.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingMonitoring.API.Data.Repositories
{
    public class AlertConfigRepository : IAlertConfigRepository
    {
        private readonly ApplicationDbContext _context;

        public AlertConfigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlertConfiguration>> GetAllAsync()
        {
            return await _context.AlertConfigurations.ToListAsync();
        }

        public async Task AddAsync(AlertConfiguration config)
        {
            _context.AlertConfigurations.Add(config);
            await _context.SaveChangesAsync();
        }
    }
}
