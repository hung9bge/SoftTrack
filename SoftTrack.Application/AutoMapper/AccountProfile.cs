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
                  
            CreateMap<Account, AccountUpdateDto>();
             
            CreateMap<Account, AccountCreateDto>();

            CreateMap<AccountUpdateDto, Account>();
            
            CreateMap<AccountDto, Account>()
              ;
        }
    }
}
