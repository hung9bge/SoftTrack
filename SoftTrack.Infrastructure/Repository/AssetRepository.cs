//using Microsoft.EntityFrameworkCore;
//using SoftTrack.Domain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SoftTrack.Infrastructure
//{
//    public class AssetRepository : IAssetRepository
//    {
//        private readonly soft_track5Context _context;
//        public AssetRepository(soft_track5Context context)
//        {
//            _context = context;
//        }
//        public async Task<List<Asset>> GetAllAssetsAsync()
//        {
//            using var context = _context;
//            var lst = await _context.Assets.ToListAsync();
//            return lst;
//        }

//        public async Task<bool> IsDuplicateAsync(Asset item)
//        {
//            // Thực hiện kiểm tra trong cơ sở dữ liệu
//            var existing = await _context.Assets
//                .FirstOrDefaultAsync(s => s.SerialNumber == item.SerialNumber);

//            return existing != null;
//        }
//        public async Task CreateAssetAsync(Asset item)
//        {
//            if (await IsDuplicateAsync(item))
//            {
//                ////
//                return;
//            }

//            _context.Assets.Add(item);
//            await _context.SaveChangesAsync();
//        }
//        public async Task UpdateAssetAsync(int id, Asset item)
//        {
//            var tmp = await _context.Assets.FindAsync(id);

//            if (tmp == null)
//            {
//                //////
//                return;
//            }

//            {

//                if (item.Name != null && item.Name != "string")
//                {
//                    tmp.Name = item.Name;
//                }

//                if (item.Cpu != null && item.Cpu != "string")
//                {
//                    tmp.Cpu = item.Cpu;
//                }

//                if (item.Gpu != null && item.Gpu != "string")
//                {
//                    tmp.Gpu = item.Gpu;
//                }

//                if (item.Ram != null && item.Ram != "string")
//                {
//                    tmp.Ram = item.Ram;
//                }

//                if (item.Memory != null && item.Memory != "string")
//                {
//                    tmp.Memory = item.Memory;
//                }

//                if (item.Os != null && item.Os != "string")
//                {
//                    tmp.Os = item.Os;
//                }

//                if (item.Version != null && item.Version != "string")
//                {
//                    tmp.Version = item.Version;
//                }

//                if (item.IpAddress != null && item.IpAddress != "string")
//                {
//                    tmp.IpAddress = item.IpAddress;
//                }

//                if (item.Bandwidth != null && item.Bandwidth != "string")
//                {
//                    tmp.Bandwidth = item.Bandwidth;
//                }

//                if (item.Manufacturer != null && item.Manufacturer != "string")
//                {
//                    tmp.Manufacturer = item.Manufacturer;
//                }

//                if (item.Model != null && item.Model != "string")
//                {
//                    tmp.Model = item.Model;
//                }

//                if (item.SerialNumber != null && item.SerialNumber != "string")
//                {
//                    tmp.SerialNumber = item.SerialNumber;
//                }

//                if (item.LastSuccesfullScan != null)
//                {
//                    tmp.LastSuccesfullScan = item.LastSuccesfullScan;
//                }

//                if (item.Status != 0)
//                {
//                    tmp.Status = item.Status;
//                }

//                // Lưu thay đổi vào cơ sở dữ liệu
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task<Asset> GetAssetsByIdAsync(int id)
//        {
//            using var context = _context;
//            var ass = await _context.Assets.FindAsync(id);
//            return ass;
//        }
//        public async Task DeleteAssetByIdAsync(int id)
//        {
//            var tmp = await _context.Assets.FindAsync(id);

//            if (tmp != null)
//            {
//                // Thay đổi trường Status của Device thành 3
//                tmp.Status = 3;

//                // Lưu thay đổi vào cơ sở dữ liệu
//                await _context.SaveChangesAsync();
//            }
//        }
//        public async Task<List<Asset>> GetAssetsForAppAsync(int appId)
//        {
//            using var context = _context;
//            var lst = await _context.AssetApplications
//                 .Where(asset => asset.AppId == appId)
//                 .Select(asset => asset.Asset)
//                 .ToListAsync();

//            return lst;
//        }
//    }
//}
