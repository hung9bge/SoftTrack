
using System.Threading.Tasks;

namespace SoftTrack.Domain
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllDeviceAsync();
        Task CreateDeviceAsync(Device device);
        Task UpdateDeviceAsync(Device device);
        Task DeleteSoftwareByDeviceIdAsync(int deviceId);
        Task<List<Device>> GetAllDeviceWithSoftwaresAsync();
        Task<List<Device>> GetDevicesForAccountAsync(int accountId);
    }
}
