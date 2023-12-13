using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly soft_track5Context _context;
        private readonly IConfiguration _configuration;
        public AccountController( IConfiguration configuration, soft_track5Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("ListAccount")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
        {

            var accounts = await _context.Accounts
                .Include(account => account.Role)
                .OrderBy(account => account.Status)
                .Select(account => new AccountDto
                {
                    AccId = account.AccId,
                    Email = account.Email,
                    Name = account.Name,
                    Status = account.Status,
                    RoleId = account.RoleId,
                    RoleName = account.Role.Name
                })
                .ToListAsync();
            if (accounts == null )
            {
                //Không tìm thấy tài khoản nào
                return NotFound();
            }
            return accounts;
        }
        [HttpGet("SearchByEmail")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccountsByEmail(string email)
        {
            //Truy vấn danh sách các tài khoản có email trùng với email đã cho
            var accounts = await _context.Accounts
                .Include(account => account.Role)
                .Where(account => account.Email == email)
                .Select(account => new AccountDto
                {
                    AccId = account.AccId,
                    Email = account.Email,
                    Name = account.Name,
                    Status = account.Status,
                    RoleId = account.RoleId,
                    RoleName = account.Role.Name
                })
                .ToListAsync();

            if (accounts == null)
            {
                //Không tìm thấy tài khoản trùng khớp
                return NotFound();
            }

            return accounts;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] string email)
        {
            if (!email.EndsWith("@fpt.edu.vn", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound();
            }
            var user = await _context.Accounts
            .Where(u => u.Email.ToLower() == email.ToLower())
            .Include(u => u.Role) // Kết hợp thông tin về Role
            .FirstOrDefaultAsync(); // Sử dụng FirstOrDefaultAsync để lấy một bản ghi hoặc null nếu không tìm thấy

            if (user == null)
            {
                return NotFound();
            }

            var roles = user.Role.Name;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JwtIssuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.AccId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roles)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            AccountDto response = new()
            {
                Name = user.Name,
                Email = user.Email,
                AccId = user.AccId,
                RoleId = user.RoleId,
                RoleName = user.Role.Name,
                Status = user.Status,
            };

            response.token = tokenHandler.WriteToken(token);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> AddAccount([FromBody] AccountCreateDto accountDto)
        {
            if (!accountDto.Email.EndsWith("@fpt.edu.vn"))
            {
                // Nếu không có đuôi "@fpt.edu.vn", thêm đuôi vào email
                accountDto.Email += "@fpt.edu.vn";
            }

            // Sử dụng Entity Framework Core để kiểm tra sự tồn tại của tài khoản
            var existingAccount = _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefault(a => a.Email == accountDto.Email && a.Role.RoleId == accountDto.RoleId);

            if (existingAccount != null)
            {
                return NotFound();
            }

            // Tạo đối tượng Account từ DTO
            var newAccount = new Account
            {
                Name = accountDto.Name,
                Email = accountDto.Email,
                RoleId = accountDto.RoleId,
                Status = accountDto.Status
            };

            // Thêm tài khoản mới vào cơ sở dữ liệu
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("DeleteAccountWith_key")]
        public async Task<IActionResult> DeleteAccountAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null || account.Status == 3)
            {
                return NotFound();
            }
            else
            {
                account.Status = 3;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPut("Update_Accpunt{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountUpdateDto accountDto)
        {
            var existingAccount = await _context.Accounts.FindAsync(id);

            if (existingAccount == null)
            {
                return NotFound();
            }

            if (accountDto.Email != "string")
            {
                existingAccount.Email = accountDto.Email;
            }
            // Cập nhật thông tin tài khoản với dữ liệu từ yêu cầu
            if (!accountDto.Email.EndsWith("@fpt.edu.vn"))
            {
                // Nếu không có đuôi "@fpt.edu.vn", thêm đuôi vào email
                accountDto.Email += "@fpt.edu.vn";
            }
            if (accountDto.Name != "string")
            {
                existingAccount.Name = accountDto.Name;
            }
           
                existingAccount.Status = accountDto.Status;
            if (accountDto.RoleId != 0)
            {
                existingAccount.RoleId = accountDto.RoleId;
            }
  
            // Các trường cần cập nhật khác nếu có

            _context.Accounts.Update(existingAccount);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
