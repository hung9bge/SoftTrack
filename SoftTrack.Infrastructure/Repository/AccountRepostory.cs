using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;

namespace SoftTrack.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        private readonly soft_trackContext _context;
        public AccountRepository(soft_trackContext context)
        {
            _context = context;
        }
        public async Task<List<Account>> GetAllAccountAsync()
        {
            using var context = _context;
            var listAccounts = await _context.Accounts.ToListAsync();
            return listAccounts;
        }
        public async Task CreateAccountAsync(Account Account)
        {
            using var context = _context;
            _context.Accounts.Add(Account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account Account)
        {
            using var context = _context;
            _context.Entry(Account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Account Account)
        {
            using var context = _context;
            _context.Accounts.Remove(Account);
            await _context.SaveChangesAsync();
        }
    }
}
