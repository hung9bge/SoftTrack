using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftTrack.Application.DTO;
using SoftTrack.Application.DTO.Report;
using SoftTrack.Application.Interface;
using SoftTrack.Application.Service;
using SoftTrack.Domain;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly soft_track2Context _context;

        private readonly IAccountService _repo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(IAccountService userRepository, IConfiguration configuration, IMapper mapper, soft_track2Context context)
        {
            _repo = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet("ListAccount")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
        {

            var accounts = await _context.Accounts
                .Include(account => account.Role)
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

            if (accounts == null || accounts.Count == 0)
            {
                //Không tìm thấy tài khoản trùng khớp
                return NotFound("Không tìm thấy tài khoản trùng khớp.");
            }

            return accounts;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] string email)
        {
            if (!email.EndsWith("@fpt.edu.vn", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Email không có đuôi @fpt.edu.vn.");
            }

            var user = await _repo.Login(email);

            if (user == null)
            {
                return BadRequest("Email không tồn tại hoặc sai mật khẩu.");
            }

            var roles = user.RoleName;

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

            var response = _mapper.Map<AccountDto>(user);

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
                return BadRequest("Email và vai trò đã tồn tại trong cơ sở dữ liệu.");
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

            return Ok("Tài khoản đã được thêm thành công.");
        }

        [HttpDelete("DeleteAccountWith_key")]
        public async Task<IActionResult> DeleteAccountAsync(int accountId) {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account != null)
            {
                account.Status = false;

                await _context.SaveChangesAsync();
            }
            return Ok("Tài khoản đã được update role thành công.");
        } 
    


    [HttpPut("Update_Accpunt{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountUpdateDto accountDto)
        {
            var existingAccount = await _context.Accounts.FindAsync(id);

            if (existingAccount == null)
            {
                return NotFound("Tài khoản không tồn tại.");
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

            return Ok("Tài khoản đã được cập nhật thành công.");
        }
    }
}
