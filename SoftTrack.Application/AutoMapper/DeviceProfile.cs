using AutoMapper;
using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.AutoMapper
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDto>();
            CreateMap<DeviceDto, Device>();
            CreateMap<DeviceCreateDto, Device>();
    
            CreateMap<DeviceUpdateDto, Device>();
            CreateMap<Device, DeviceDto>();
            CreateMap<Software, SoftwaresDeviceDto>();
            CreateMap<Device, DeviceDto>()
                /*.ForMember(dest => dest.Softwares, opt => opt.MapFrom(src => src.Softwares))*/;

        }
    }
}
