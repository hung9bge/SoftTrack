using AutoMapper;
using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.AutoMapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>()
                  .ForMember(dest => dest.RoleAccounts, opt => opt.MapFrom(src => src.RoleAccounts));
            CreateMap<Account, AccountUpdateDto>();
             
            CreateMap<Account, AccountCreateDto>();

            CreateMap<AccountUpdateDto, Account>();
            CreateMap<RoleAccount, RoleAccountDto>();
            CreateMap<AccountDto, Account>()
              ;
        }
    }
}
