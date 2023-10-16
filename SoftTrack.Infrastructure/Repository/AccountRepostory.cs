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
            var accountsWithRoleAccounts = await _context.Accounts
       .Include(account => account.RoleAccounts)
       .ThenInclude(roleAccount => roleAccount.Role)
       .ToListAsync();

            return accountsWithRoleAccounts;
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

        public async Task<Account?> Login(string email)
        {
            try
            {
                using var context = _context;
                var user = await _context.Accounts.AsNoTracking().Where(user => user.Email.ToLower() == email.ToLower())
                .Include(user => user.RoleAccounts).FirstOrDefaultAsync();
                if (user == null) { return null; }

                return user;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task Register(Account User)
        {
            try
            {
                using var context = _context;
                var mem = new Account()
                {
                    Email = User.Email,
                    Password = User.Password,
                    PhoneNumber = User.PhoneNumber,
                    Name = User.Name,
                    Account1 = User.Account1,

                };
                await _context.AddAsync(mem);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    
    }
}
