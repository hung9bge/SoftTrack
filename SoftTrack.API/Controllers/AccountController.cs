using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftTrack.Application.DTO;
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
        private readonly soft_trackContext _context;

        private readonly IAccountService _repo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
<<<<<<< HEAD
=======


        [HttpGet]
        public async Task<IActionResult> GetAllAccountAsync()
        {
            var ressult = await _repo.GetAllAccountAsync();
            return StatusCode(StatusCodes.Status200OK, ressult);
        }
>>>>>>> 2e4737f53d7ea807931aa8c24da062455f4264ad

        public AccountController(IAccountService userRepository, IConfiguration configuration, IMapper mapper, soft_trackContext context)
        {
            _repo = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountCreateDto>>> GetAccounts()
        {
            var accounts = await _context.Accounts
                .Include(account => account.RoleAccounts)
                .ThenInclude(roleAccount => roleAccount.Role)
                .Select(account => new AccountCreateDto
                {
                    Account1 = account.Account1,
                    Email = account.Email,
                    Role_Name = account.RoleAccounts.FirstOrDefault().Role.Name
                })
                .ToListAsync();

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
            var role = user.RoleAccounts.Select(ra => ra.RoleId).FirstOrDefault().ToString();

<<<<<<< HEAD
=======

>>>>>>> 2e4737f53d7ea807931aa8c24da062455f4264ad
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JwtIssuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.AccId.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, role)
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
        public async Task<IActionResult> AddAccount([FromBody] AccountUpdateDto accountDto)
        {
<<<<<<< HEAD

            // Tạo đối tượng Account từ DTO
            var newAccount = new Account
            {             
                Account1 = accountDto.Account1,
                Email = accountDto.Email,
            };

            // Tạo RoleAccount từ Role_Name (nếu cần)
            if (!string.IsNullOrEmpty(accountDto.Role_Name))
            {
                var role = _context.Roles.FirstOrDefault(r => r.Name == accountDto.Role_Name);
                if (role == null)
                {
                    return BadRequest("Role không tồn tại.");
                }

                var roleAccount = new RoleAccount
                {
                    RoleId = role.Id,
                    Acc = newAccount
                };

                newAccount.RoleAccounts.Add(roleAccount);
            }

            // Thêm tài khoản mới vào cơ sở dữ liệu
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            return Ok("Tài khoản đã được thêm thành công.");
        }
        [HttpPut("Update_Accpunt{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountUpdateDto accountDto)
        {
            var existingAccount = await _context.Accounts.FindAsync(id);

            if (existingAccount == null)
            {
                return NotFound("Tài khoản không tồn tại.");
            }

            // Cập nhật thông tin tài khoản với dữ liệu từ yêu cầu
            existingAccount.Account1 = accountDto.Account1;
            existingAccount.Email = accountDto.Email;
            // Các trường cần cập nhật khác nếu có

            _context.Accounts.Update(existingAccount);
            await _context.SaveChangesAsync();

            return Ok("Tài khoản đã được cập nhật thành công.");
=======
            await _repo.Register(request);
            return Ok();
>>>>>>> 2e4737f53d7ea807931aa8c24da062455f4264ad
        }
    }
}
