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
    }
}
