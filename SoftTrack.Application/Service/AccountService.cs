//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using SoftTrack.Manage.DTO;
//using SoftTrack.Application.Interface;
//using SoftTrack.Domain;

//namespace SoftTrack.Application.Service
//{
//    public class AccountService : IAccountService
//    {
//        private readonly IAccountRepository _AccountRepository;
//        private readonly IMapper _mapper;
//        public AccountService(IAccountRepository AccountRepository, IMapper mapper)
//        {
//            _AccountRepository = AccountRepository;
//            _mapper = mapper;
//        }

//        public async Task<List<AccountDto>> GetAllAccountAsync()
//        {
//            var listAccount = await _AccountRepository.accountsWithRoleNames();         
//            var listAccountDto = _mapper.Map<List<AccountDto>>(listAccount);

//            return listAccountDto;
//        }
//        public async Task CreateAccountAsync(AccountCreateDto AccountCreateDto)
//        {
//            var Account = _mapper.Map<Account>(AccountCreateDto);
//            await _AccountRepository.CreateAccountAsync(Account);
//        }

            
//        public async Task UpdateAccountAsync(AccountUpdateDto AccountUpdateDto)
//        {
//            var Account = _mapper.Map<Account>(AccountUpdateDto);
//            await _AccountRepository.UpdateAccountAsync(Account);
//        }

//        public async Task DeleteAccountAsync(AccountDto AccountDto)
//        {
//            var Account = _mapper.Map<Account>(AccountDto);
//            await _AccountRepository.DeleteAccountAsync(Account);
//        }

//        public async Task<AccountDto> Login(string email)
//        {
//            var user = await _AccountRepository.Login(email);
           
//            if (user == null)
//            {
//                return null;
//            }

//            // Chuyển đổi từ Account thành AccountDto bằng AutoMapper
//            var userDto = _mapper.Map<AccountDto>(user);

//            return userDto;
//        }


//        public async Task Register(AccountCreateDto member)
//        {
//            var Account = _mapper.Map<Account>(member);
//            await _AccountRepository.Register(Account);
//        }
//    }
//}
