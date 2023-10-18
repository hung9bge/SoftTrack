using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.Interface
{
    public interface IDeviceService
    {
        Task<List<DeviceDto>> GetAllDeviceAsync();
        Task CreateDeviceAsync(DeviceCreateDto Device);
        Task UpdateDeviceAsync(DeviceUpdateDto Device);
        Task DeleteDeviceAsync(int DeviceId);
        Task<List<DeviceDto>> GetAllDeviceWithSoftwaresAsync();
        Task<List<DeviceDto>> GetDevicesForAccountAsync(int accountId);

    }
}
