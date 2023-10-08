﻿using Microsoft.Win32;
using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.Application.Interface
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAllAccountAsync();
        Task CreateAccountAsync(AccountCreateDto Account);
        Task UpdateAccountAsync(AccountUpdateDto Account);
        Task DeleteAccountAsync(AccountDto Account);

        Task<Account> Login(string email, string password);
        Task Register(AccountCreateDto member);

    }
}
