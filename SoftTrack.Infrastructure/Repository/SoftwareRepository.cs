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

        public async Task DeleteSoftwareAsync(int softwareId)
        {
            var software = await _context.Softwares.FindAsync(softwareId);

            if (software != null)
            {
                _context.Softwares.Remove(software);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Software>> GetSoftwareForAccountAsync(int accountId)
        {
            // Thực hiện truy vấn để lấy danh sách phần mềm cho tài khoản có accountId cụ thể
            var softwaresForAccount = await _context.Softwares
                .Where(software => software.Device.AccId == accountId) // Lọc theo ID tài khoản
                .ToListAsync();

            return softwaresForAccount;
        }
    }
}
