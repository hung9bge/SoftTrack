
namespace SoftTrack.Domain
{
    public interface IDeviceRepostory
    {
        Task<List<Device>> GetAllDeviceAsync();
        Task CreateDeviceAsync(Device device);
        Task UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(Device device);
    }
}
