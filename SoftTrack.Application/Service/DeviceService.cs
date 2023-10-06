using AutoMapper;
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

        public async Task DeleteDeviceAsync(DeviceDto DeviceDto)
        {
            var Device = _mapper.Map<Device>(DeviceDto);
            await _DeviceRepository.DeleteDeviceAsync(Device);
        }
    }
}
