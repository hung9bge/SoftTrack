using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.Interface
{
    public interface ISoftwareService
    {
        Task<List<SoftwareDto>> GetAllSoftwareAsync();
        Task CreateSoftwareAsync(SoftwareCreateDto software);
        Task UpdateSoftwareAsync(int softwareId, SoftwareUpdateDto updatedSoftware);
        Task DeleteSoftwareAsync(int softwareId);
        Task<List<SoftwareDto>> GetSoftwareForAccountAsync(int accountId);
        //Task<List<SoftwareDto>> GetSoftwareForDeviceAsync(int deviceId);
    }
}
