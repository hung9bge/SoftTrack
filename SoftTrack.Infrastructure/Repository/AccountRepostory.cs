//using Microsoft.EntityFrameworkCore;
//using SoftTrack.Domain;

//namespace SoftTrack.Infrastructure
//{
//    public class AccountRepository : IAccountRepository
//    {
//        private readonly soft_track4Context _context;
//        public AccountRepository(soft_track4Context context)
//        {
//            _context = context;
//        }
//        public async Task<List<Account>> accountsWithRoleNames()
//        {
//            using var context = _context;
//            var accountsWithRoleNames = await _context.Accounts
//                .Include(account => account.Role) // Sử dụng Include để lấy thông tin về Role
//                .ToListAsync();

//            return accountsWithRoleNames;
//        }
//        public async Task CreateAccountAsync(Account Account)
//        {
//            using var context = _context;
//            _context.Accounts.Add(Account);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateAccountAsync(Account Account)
//        {
//            using var context = _context;
//            _context.Entry(Account).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteAccountAsync(Account Account)
//        {
//            using var context = _context;
//            _context.Accounts.Remove(Account);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<Account> Login(string email)
//        {
//            try
//            {
//                using var context = _context;

//                var user = await _context.Accounts
//             .Where(u => u.Email.ToLower() == email.ToLower())
//             .Include(u => u.Role) // Kết hợp thông tin về Role
//             .FirstOrDefaultAsync(); // Sử dụng FirstOrDefaultAsync để lấy một bản ghi hoặc null nếu không tìm thấy


//                return user;
//            }

//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task Register(Account User)
//        {
//            try
//            {
//                using var context = _context;
//                var mem = new Account()
//                {
//                    Email = User.Email,
//                    Status = User.Status,
//                    RoleId = User.RoleId,
//                    Name = User.Name,


//                };
//                await _context.AddAsync(mem);
//                await _context.SaveChangesAsync();

//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }


//    }
//}
