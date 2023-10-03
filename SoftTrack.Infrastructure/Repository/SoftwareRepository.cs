using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;

namespace SoftTrack.Infrastructure
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly soft_trackContext _context;
        public SoftwareRepository(soft_trackContext context)
        {
            _context = context;
        }
        public async Task<List<Software>> GetAllSoftwareAsync()
        {
            using var context = _context;
            var listSoftwares = await _context.Softwares.ToListAsync();
            return listSoftwares;
        }
        public async Task CreateSoftwareAsync(Software software)
        {
            using var context = _context;
            _context.Softwares.Add(software);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSoftwareAsync(Software software)
        {
            using var context = _context;
            _context.Entry(software).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSoftwareAsync(Software software)
        {
            using var context = _context;
            _context.Softwares.Remove(software);
            await _context.SaveChangesAsync();
        }
    }
}
