

namespace SoftTrack.Domain
{
    public interface ISoftwareRepository
    {
        Task<List<Software>> GetAllSoftwareAsync();
        Task CreateSoftwareAsync(Software software);
        Task UpdateSoftwareAsync(Software software);
        Task DeleteSoftwareAsync(Software software);
        Task<List<Software>> GetSoftwareForAccountAsync(int accountId);
    }
}
