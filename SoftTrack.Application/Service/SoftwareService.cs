using AutoMapper;
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

        public async Task UpdateSoftwareAsync(SoftwareUpdateDto softwareUpdateDto)
        {
            var software = _mapper.Map<Software>(softwareUpdateDto);
            await _softwareRepository.UpdateSoftwareAsync(software);
        }

        public async Task DeleteSoftwareAsync(SoftwareDto softwareDto)
        {
            var software = _mapper.Map<Software>(softwareDto);
            await _softwareRepository.DeleteSoftwareAsync(software);
        }
    }
}
