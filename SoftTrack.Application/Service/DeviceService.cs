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
            var listDeviceDto = _mapper.Map<List<DeviceDto>>(listDevice);
            return listDeviceDto;
        }
        public async Task CreateDeviceAsync(DeviceCreateDto DeviceCreateDto)
        {
            var Device = _mapper.Map<Device>(DeviceCreateDto);
            await _DeviceRepository.CreateDeviceAsync(Device);
        }

        public async Task UpdateDeviceAsync(DeviceUpdateDto DeviceUpdateDto)
        {
            var Device = _mapper.Map<Device>(DeviceUpdateDto);
            await _DeviceRepository.UpdateDeviceAsync(Device);
        }

        public async Task DeleteDeviceAsync(int DeviceId)
        {
           
            await _DeviceRepository.DeleteSoftwareByDeviceIdAsync(DeviceId);
        }
        public async Task<List<DeviceDto>> GetAllDeviceWithSoftwaresAsync()
        {
            var devicesWithSoftwares = await _DeviceRepository.GetAllDeviceWithSoftwaresAsync();
            var devicesDtoList = new List<DeviceDto>();

            foreach (var device in devicesWithSoftwares)
            {
                var deviceDto = _mapper.Map<DeviceDto>(device);
                devicesDtoList.Add(deviceDto);
            }

            return devicesDtoList;
        }
        public async Task<List<DeviceDto>> GetDevicesForAccountAsync(int accountId)
        {
            // Sử dụng phương thức GetDevicesForAccountAsync để lấy danh sách Device
            var devicesForAccount = await _DeviceRepository.GetDevicesForAccountAsync(accountId);

            // Ánh xạ danh sách Device thành danh sách DeviceDto bằng AutoMapper
            var deviceDtos = _mapper.Map<List<DeviceDto>>(devicesForAccount);

            return deviceDtos;
        }
    }
}
