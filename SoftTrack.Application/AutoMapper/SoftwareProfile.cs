using AutoMapper;
using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.AutoMapper
{
    public class SoftwareProfile : Profile
    {
        public SoftwareProfile()
        {
            //CreateMap<Software, SoftwareDto>();
            CreateMap<SoftwareDto, Software>();
            CreateMap<SoftwareCreateDto, Software>();
    
            CreateMap<SoftwareUpdateDto, Software>();
            CreateMap<Software, SoftwareDto>()
    .ForMember(dest => dest.InstallDate, opt => opt.Ignore()) // Loại bỏ ánh xạ cho Device
    /*.ForMember(dest => dest.Issues, opt => opt.Ignore())*/; 


        }
    }
}
