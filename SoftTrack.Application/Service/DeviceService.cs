﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Application.DTO;
using SoftTrack.Application.DTO.Report;
using SoftTrack.Application.Interface;
using SoftTrack.Domain;
using System.Globalization;

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
                Os = device.Os,
                Version = device.Version,
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
            var device = new Device();
            device.Name = updatedDevice.Name;
            device.Cpu = updatedDevice.Cpu;
            device.Gpu = updatedDevice.Gpu;
            device.Ram = updatedDevice.Ram;
            device.Memory = updatedDevice.Memory;
            device.Os = updatedDevice.Os;
            device.Version = updatedDevice.Version;
            device.IpAddress = updatedDevice.IpAddress;
            device.Manufacturer = updatedDevice.Manufacturer;
            device.Model = updatedDevice.Model;
            device.SerialNumber = updatedDevice.SerialNumber;
            device.Status = updatedDevice.Status;
            if (DateTime.TryParseExact(updatedDevice.LastSuccesfullScan, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                device.LastSuccesfullScan = parsedDate;
            }
            await _DeviceRepository.UpdateDeviceAsync( deviceId, device);
        }

        public async Task DeleteDeviceAsync(int DeviceId)
        {
           
           await _DeviceRepository.DeleteSoftwareByDeviceIdAsync(DeviceId);
        }
        public async Task<List<DeviceDto>> GetDevicesForAccountAsync(int accountId)
        {
            var devicesForAccount = await _DeviceRepository.GetDevicesForAccountAsync(accountId);

            var listdeviceDtos = devicesForAccount.Select(device => new DeviceDto
            {
                DeviceId = device.DeviceId,
                Name = device.Name,
                Cpu = device.Cpu,
                Gpu = device.Gpu,
                Ram = device.Ram,
                Memory = device.Memory,
                Os = device.Os,
                Version = device.Version,
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
                Os = device.Os,
                Version = device.Version,
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
