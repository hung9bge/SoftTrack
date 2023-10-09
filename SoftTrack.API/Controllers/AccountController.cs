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
       

        [HttpGet]
        public async Task<IActionResult> GetAllAccountAsync()
        {
            var ressult = await _repo.GetAllAccountAsync();
            return StatusCode(StatusCodes.Status200OK, ressult);
        }

        public AccountController(IAccountService userRepository, IConfiguration configuration, IMapper mapper, soft_trackContext context)
        {
            _repo = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AccountCreateDto>> UserLogin(AccountDto model)
        {
            var user = await _repo.Login(model.Email, model.Password);
            if (user == null) return NotFound();
            var role = user.RoleAccounts.Select(ra => ra.RoleId).FirstOrDefault().ToString();

           
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

            var response = _mapper.Map<AccountCreateDto>(user);

            response.token = tokenHandler.WriteToken(token);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAccount([FromBody] AccountCreateDto request)
        {
            await _repo.Register(request);
           return Ok();
        }
    }
}
