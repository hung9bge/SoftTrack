using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.Interface
{
    public interface IDeviceService
    {
        Task<List<DeviceDto>> GetAllDeviceAsync();
        Task CreateDeviceAsync(DeviceCreateDto Device);
        Task UpdateDeviceAsync(int deviceId, DeviceUpdateDto updatedDevice);
        Task DeleteDeviceAsync(int DeviceId);
        Task<List<DeviceDto>> GetDevicesForAccountAsync(int accountId);
        Task<List<DeviceDto>> GetDevicesForSoftWareAsync(int softwareId);

    }
}
