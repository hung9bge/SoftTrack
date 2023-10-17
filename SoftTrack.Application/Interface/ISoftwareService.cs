using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.Interface
{
    public interface ISoftwareService
    {
        Task<List<SoftwareDto>> GetAllSoftwareAsync();
        Task CreateSoftwareAsync(SoftwareCreateDto software);
        Task UpdateSoftwareAsync(SoftwareUpdateDto software);
        Task DeleteSoftwareAsync(SoftwareDto software);
        Task<List<SoftwareDto>> GetSoftwareForAccountAsync(int accountId);
    }
}
