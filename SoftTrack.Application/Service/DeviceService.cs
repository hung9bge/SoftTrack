using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Application.DTO;
using SoftTrack.Application.Interface;
using SoftTrack.Domain;

namespace SoftTrack.Application.Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _DeviceRepository;
        private readonly IMapper _mapper;
        public DeviceService(IDeviceRepository DeviceRepository, IMapper mapper)
        {
            _DeviceRepository = DeviceRepository;
            _mapper = mapper;
        }

        public async Task<List<DeviceDto>> GetAllDeviceAsync()
        {
            var listDevice = await _DeviceRepository.GetAllDeviceAsync();
            var listdeviceDtos = listDevice.Select(device => new DeviceDto
            {
                DeviceId = device.DeviceId,
                Name = device.Name,
                Cpu = device.Cpu,
                Gpu = device.Gpu,
                Ram = device.Ram,
                Memory = device.Memory,
                IpAddress = device.IpAddress,
                Manufacturer = device.Manufacturer,
                Model = device.Model,
                SerialNumber = device.SerialNumber,
                // Định dạng LastSuccesfullScan sang chuỗi ngày/tháng/năm
                LastSuccesfullScan = device.LastSuccesfullScan?.Date.ToString("dd/MM/yyyy"),
                Status = device.Status
            }).ToList();
            return listdeviceDtos;
        }
        public async Task CreateDeviceAsync(DeviceCreateDto DeviceCreateDto)
        {
         
            var Device = _mapper.Map<Device>(DeviceCreateDto);

            await _DeviceRepository.CreateDeviceAsync(Device);
        }

        public async Task UpdateDeviceAsync(int deviceId, DeviceUpdateDto updatedDevice)
        {
            var Device = _mapper.Map<Device>(updatedDevice);
            await _DeviceRepository.UpdateDeviceAsync( deviceId, Device);
        }

        public async Task DeleteDeviceAsync(int DeviceId)
        {
           
           await _DeviceRepository.DeleteSoftwareByDeviceIdAsync(DeviceId);
        }
    
        public async Task<List<DeviceDto>> GetDevicesForSoftWareAsync(int softwareId)
        {
            // Sử dụng phương thức GetDevicesForAccountAsync để lấy danh sách Device
            var devicesForAccount = await _DeviceRepository.GetDevicesForSoftWareAsync(softwareId);        

            var listdeviceDtos = devicesForAccount.Select(device => new DeviceDto
            {
                DeviceId = device.DeviceId,
                Name = device.Name,
                Cpu = device.Cpu,
                Gpu = device.Gpu,
                Ram = device.Ram,
                Memory = device.Memory,
                IpAddress = device.IpAddress,
                Manufacturer = device.Manufacturer,
                Model = device.Model,
                SerialNumber = device.SerialNumber,
                // Định dạng LastSuccesfullScan sang chuỗi ngày/tháng/năm
                LastSuccesfullScan = device.LastSuccesfullScan?.Date.ToString("dd/MM/yyyy"),
                Status = device.Status
            }).ToList();
            return listdeviceDtos;
        }
    }
}
