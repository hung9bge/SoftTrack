//using Microsoft.EntityFrameworkCore;
//using SoftTrack.Domain;
//using System.Globalization;

//namespace SoftTrack.Infrastructure
//{
//    public class DeviceRepository : IDeviceRepository
//    {
//        private readonly soft_track4Context _context;
//        public DeviceRepository(soft_track4Context context)
//        {
//            _context = context;
//        }
//        public async Task<List<Device>> GetAllDeviceAsync()
//        {
//            using var context = _context;
//            var listDevices = await _context.Devices.ToListAsync();
//            return listDevices;
//        }
//        public async Task CreateDeviceAsync(Device Device)
//        {
//            using var context = _context;
//            Device.Status = 1;
//            _context.Devices.Add(Device);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateDeviceAsync(int deviceId, Device updatedDevice)
//        {
//            // Tìm thiết bị dựa trên ID
//            var device = await _context.Devices.FindAsync(deviceId);

//            if (device == null)
//            {
//                // Không tìm thấy thiết bị
//                // Ở đây, bạn có thể xử lý việc không tìm thấy thiết bị (ví dụ: trả về lỗi hoặc thông báo)
//            }
//            else
//            {
//                // Cập nhật các trường cần thiết của thiết bị
//                if (updatedDevice.Name != null && updatedDevice.Name != "string")
//                {
//                    device.Name = updatedDevice.Name;
//                }

//                if (updatedDevice.Cpu != null && updatedDevice.Cpu != "string")
//                {
//                    device.Cpu = updatedDevice.Cpu;
//                }

//                if (updatedDevice.Gpu != null && updatedDevice.Gpu != "string")
//                {
//                    device.Gpu = updatedDevice.Gpu;
//                }

//                if (updatedDevice.Ram != 0)
//                {
//                    device.Ram = updatedDevice.Ram;
//                }

//                if (updatedDevice.Memory != 0)
//                {
//                    device.Memory = updatedDevice.Memory;
//                }
//                if (updatedDevice.Os != null && updatedDevice.Os != "string")
//                {
//                    device.Os = updatedDevice.Os;
//                }
//                if (updatedDevice.Version != null && updatedDevice.Version != "string")
//                {
//                    device.Version = updatedDevice.Version;
//                }
//                if (updatedDevice.IpAddress != null && updatedDevice.IpAddress != "string")
//                {
//                    device.IpAddress = updatedDevice.IpAddress;
//                }

//                if (updatedDevice.Manufacturer != null && updatedDevice.Manufacturer != "string")
//                {
//                    device.Manufacturer = updatedDevice.Manufacturer;
//                }

//                if (updatedDevice.Model != null && updatedDevice.Model != "string")
//                {
//                    device.Model = updatedDevice.Model;
//                }

//                if (updatedDevice.SerialNumber != null && updatedDevice.SerialNumber != "string")
//                {
//                    device.SerialNumber = updatedDevice.SerialNumber;
//                }
//                if (updatedDevice.LastSuccesfullScan != null)
//                {
//                    device.LastSuccesfullScan = updatedDevice.LastSuccesfullScan;
//                }
//                if (updatedDevice.Status != 0)
//                {
//                    device.Status = updatedDevice.Status;
//                }

//                // Lưu thay đổi vào cơ sở dữ liệu
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task DeleteSoftwareByDeviceIdAsync(int deviceId)
//        {
//            var device = await _context.Devices.FindAsync(deviceId);

//            if (device != null)
//            {
//                // Thay đổi trường Status của Device thành 3
//                device.Status = 3;

//                // Lưu thay đổi vào cơ sở dữ liệu
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task<List<Device>> GetDevicesForSoftWareAsync(int softwareId)
//        {
//            using var context = _context;
//            var devicesForSoftware = await _context.DeviceSoftwares
//                   .Where(ds => ds.SoftwareId == softwareId)
//                   .Select(ds => ds.Device)
//                   .ToListAsync();

//            return devicesForSoftware;
//        }
//        public async Task<List<Device>> GetDevicesForAccountAsync(int accountId)
//        {
//            using var context = _context;
//            var devices = _context.DeviceSoftwares
//                .Where(ds => ds.Software.AccId == accountId)
//                .Select(ds => ds.Device)
//                .Distinct()
//                .ToList();

//            return devices;
//        }


//    }
//}
