using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;

namespace SoftTrack.Infrastructure
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly soft_track2Context _context;
        public SoftwareRepository(soft_track2Context context)
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
            if (await IsDuplicateSoftwareAsync(software))
            {
                // Phần mềm đã tồn tại, xử lý lỗi ở đây (ném ra ngoại lệ hoặc trả về thông báo lỗi)
            }
            else
            {
                _context.Softwares.Add(software);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsDuplicateSoftwareAsync(Software software)
        {
            // Thực hiện kiểm tra trong cơ sở dữ liệu
            var existingSoftware = await _context.Softwares
                .FirstOrDefaultAsync(s =>
                    s.Name == software.Name &&
                    s.Publisher == software.Publisher &&
                    s.Version == software.Version);

            return existingSoftware != null;
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
                .Where(software => software.AccId == accountId) // Lọc theo ID tài khoản
                .ToListAsync();

            return softwaresForAccount;
        }
        //public async Task<List<Software>> GetSoftwareForDeviceAsync(int deviceId)
        //{
        //    // Thực hiện truy vấn để lấy danh sách phần mềm cho tài khoản cụ thể
        //    var softwaresForDevice = await _context.Softwares
        //       .Where(software => software.DeviceId == deviceId) // Lọc theo ID tài khoản
        //       .ToListAsync();

        //    return softwaresForDevice;
        //}
    }
}
