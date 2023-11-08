using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Infrastructure
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly soft_track4Context _context;
        public ApplicationRepository(soft_track4Context context)
        {
            _context = context;
        }
        public async Task<List<Application>> GetAllAppsAsync()
        {
            using var context = _context;
            var listApps = await _context.Applications.ToListAsync();
            return listApps;
        }

        public async Task<bool> IsDuplicateAsync(Application item)
        {
            // Thực hiện kiểm tra trong cơ sở dữ liệu
            var existing = await _context.Applications
                .FirstOrDefaultAsync(s =>
                s.Name == item.Name &&
                    s.Publisher == item.Publisher &&
                    s.Version == item.Version);

            return existing != null;
        }
        public async Task CreateAppAsync(Application app)
        {
            if (await IsDuplicateAsync(app))
            {
                ///
                return;
            }

            _context.Applications.Add(app);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAppAsync(int appId, Application updatedApp)
        {
            var app = await _context.Applications.FindAsync(appId);

            if (app == null)
            {
                //////
            }
            else
            {
                if (updatedApp.AccId != 0)
                {
                    app.AccId = updatedApp.AccId;
                }

                if (updatedApp.Name != null && updatedApp.Name != "string")
                {
                    app.Name = updatedApp.Name;
                }

                if (updatedApp.Publisher != null && updatedApp.Publisher != "string")
                {
                    app.Publisher = updatedApp.Publisher;
                }

                if (updatedApp.Version != null && updatedApp.Version != "string")
                {
                    app.Version = updatedApp.Version;
                }

                if (updatedApp.Release != null && updatedApp.Release != "string")
                {
                    app.Release = updatedApp.Release;
                }
                
                if (updatedApp.Type != null && updatedApp.Type != "string")
                {
                    app.Type = updatedApp.Type;
                }

                if (updatedApp.Os != null && updatedApp.Os != "string")
                {
                    app.Os = updatedApp.Os;
                }

                if (updatedApp.Osversion != null && updatedApp.Osversion != "string")
                {
                    app.Osversion = updatedApp.Osversion;
                }

                if (updatedApp.Description != null && updatedApp.Description != "string")
                {
                    app.Description = updatedApp.Description;
                }

                if (updatedApp.Download != null && updatedApp.Download != "string")
                {
                    app.Download = updatedApp.Download;
                }

                if (updatedApp.Docs != null && updatedApp.Docs != "string")
                {
                    app.Docs = updatedApp.Docs;
                }

                if (updatedApp.Language != null && updatedApp.Language != "string")
                {
                    app.Language = updatedApp.Language;
                }

                if (updatedApp.Db != null && updatedApp.Db != "string")
                {
                    app.Db = updatedApp.Db;
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }
        }
        //Task DeleteSoftwareByDeviceIdAsync(int deviceId);
        public async Task<List<Application>> GetAppsForAccountAsync(int accountId)
        {
            using var context = _context;
            var appsForAccountAsync = await _context.Applications
                .Where(app => app.AccId == accountId)
                .Distinct()
                .ToListAsync();

            return appsForAccountAsync;
        }
        //Task<List<Device>> GetDevicesForSoftWareAsync(int softwareId);
    }
}
