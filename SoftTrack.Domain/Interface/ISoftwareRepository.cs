

namespace SoftTrack.Domain
{
    public interface ISoftwareRepository
    {
        Task<List<Software>> GetAllSoftwareAsync();
        Task CreateSoftwareAsync(Software software);
        Task UpdateSoftwareAsync(int softwareId, Software updatedSoftware);
        Task DeleteSoftwareAsync(int softwareId);
        Task<Software> GetSoftwareAsync(int softwareId);
        Task<List<Software>> GetSoftwareForAccountAsync(int accountId);
        //Task<List<Software>> GetSoftwareForDeviceAsync(int deviceId);
    }
}
