using AutoMapper;
using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.AutoMapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<Account, AccountUpdateDto>()
                  .ForMember(dest => dest.RoleAccounts, opt => opt.MapFrom(src => src.RoleAccounts)); 
            CreateMap<Account, AccountCreateDto>();

            CreateMap<AccountUpdateDto, Account>();
            CreateMap<Account, AccountDto>();
            CreateMap<RoleAccount, RoleAccountDto>();
            CreateMap<Account, AccountCreateDto>()
                .ForMember(dest => dest.RoleAccounts, opt => opt.MapFrom(src => src.RoleAccounts));
        }
    }
}
