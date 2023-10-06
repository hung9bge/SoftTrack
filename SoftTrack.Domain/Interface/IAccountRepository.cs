
namespace SoftTrack.Domain
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccountAsync();
        Task CreateAccountAsync(Account Account);
        Task UpdateAccountAsync(Account Account);
        Task DeleteAccountAsync(Account Account);
    }
}
