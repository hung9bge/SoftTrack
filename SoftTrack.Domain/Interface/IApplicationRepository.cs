using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Domain
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetAllAppsAsync();
        Task CreateAppAsync(Application app);
        Task UpdateAppAsync(int appId, Application updatedApp);
        //Task DeleteSoftwareByDeviceIdAsync(int deviceId);
        Task<List<Application>> GetAppsForAccountAsync(int accountId);
        //Task<List<Device>> GetDevicesForSoftWareAsync(int softwareId);
    }
}
