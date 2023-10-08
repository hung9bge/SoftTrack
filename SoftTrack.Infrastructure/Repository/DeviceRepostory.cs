using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;

namespace SoftTrack.Infrastructure
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly soft_trackContext _context;
        public DeviceRepository(soft_trackContext context)
        {
            _context = context;
        }
        public async Task<List<Device>> GetAllDeviceAsync()
        {
            using var context = _context;
            var listDevices = await _context.Devices.ToListAsync();
            return listDevices;
        }
        public async Task CreateDeviceAsync(Device Device)
        {
            using var context = _context;
            _context.Devices.Add(Device);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeviceAsync(Device Device)
        {
            using var context = _context;
            _context.Entry(Device).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(Device Device)
        {
            using var context = _context;
            _context.Devices.Remove(Device);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Device>> GetAllDeviceWithSoftwaresAsync()
        {
            using var context = _context;
            var devicesWithSoftwares = await _context.Devices
                .Include(device => device.Softwares) // Kết hợp danh sách Softwares cho mỗi Device
                .ToListAsync();

            return devicesWithSoftwares;
        }
        public async Task<List<Device>> GetDevicesForAccountAsync(int accountId)
        {
            using var context = _context;

            // Lấy danh sách các thiết bị cho tài khoản có accountId cụ thể
            var devicesForAccount = await _context.Devices
                .Where(device => device.AccId == accountId)
                .ToListAsync();

            return devicesForAccount;
        }
    }
}
