using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Application.DTO;
using SoftTrack.Application.Interface;
using SoftTrack.Domain;

namespace SoftTrack.Application.Service
{
    public class SoftwareService : ISoftwareService
    {
        private readonly ISoftwareRepository _softwareRepository;
        private readonly IMapper _mapper;
        public SoftwareService(ISoftwareRepository softwareRepository, IMapper mapper)
        {
            _softwareRepository = softwareRepository;
            _mapper = mapper;
        }

        public async Task<List<SoftwareDto>> GetAllSoftwareAsync()
        {
            var listSoftware = await _softwareRepository.GetAllSoftwareAsync();
            var listSoftwareDto =  _mapper.Map<List<SoftwareDto>>(listSoftware);
            return listSoftwareDto;
        }
        public async Task CreateSoftwareAsync(SoftwareCreateDto softwareCreateDto)
        {
            var software = _mapper.Map<Software>(softwareCreateDto);
            await _softwareRepository.CreateSoftwareAsync(software);
        }
  
        public async Task UpdateSoftwareAsync(int softwareId, SoftwareUpdateDto updatedSoftware)
        {
            var software = _mapper.Map<Software>(updatedSoftware);
            await _softwareRepository.UpdateSoftwareAsync(softwareId,software);
        }

        public async Task DeleteSoftwareAsync(int softwareId)
        {
            await _softwareRepository.DeleteSoftwareAsync(softwareId);
        }
        public async Task<List<SoftwareDto>> GetSoftwareForAccountAsync(int accountId)
        {
            // Sử dụng phương thức GetDevicesForAccountAsync để lấy danh sách Device
            var softwaresForAccount = await _softwareRepository.GetSoftwareForAccountAsync(accountId);

            // Ánh xạ danh sách Device thành danh sách DeviceDto bằng AutoMapper
            var softwareDtos = _mapper.Map<List<SoftwareDto>>(softwaresForAccount);

            return softwareDtos;
        }
        public async Task<SoftwareDto> GetSoftwareAsync(int softwareId)
        {
            var softwaresForAccount = await _softwareRepository.GetSoftwareAsync(softwareId);

            // Ánh xạ danh sách Device thành danh sách DeviceDto bằng AutoMapper
            var softwareDtos = _mapper.Map<SoftwareDto>(softwaresForAccount);

            return softwareDtos;
        }
        //public async Task<List<SoftwareDto>> GetSoftwareForDeviceAsync(int deviceId)
        //{
        //    // Sử dụng phương thức GetDevicesForDeviceAsync để lấy danh sách Device
        //    var softwaresForDevice = await _softwareRepository.GetSoftwareForDeviceAsync(deviceId);

        //    // Ánh xạ danh sách Device thành danh sách DeviceDto bằng AutoMapper
        //    var softwareDtos = _mapper.Map<List<SoftwareDto>>(softwaresForDevice);

        //    return softwareDtos;
        //}

    }
}
