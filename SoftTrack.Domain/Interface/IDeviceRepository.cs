
namespace SoftTrack.Domain
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllDeviceAsync();
        Task CreateDeviceAsync(Device device);
        Task UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(Device device);
    }
}
