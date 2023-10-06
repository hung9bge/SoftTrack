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
            CreateMap<AccountDto, Account>();
            CreateMap<AccountCreateDto, Account>();
    
            CreateMap<AccountUpdateDto, Account>();
            CreateMap<Account, AccountDto>();
    


        }
    }
}
