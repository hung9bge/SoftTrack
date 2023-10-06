using Microsoft.AspNetCore.Mvc;
using SoftTrack.Application.DTO;
using SoftTrack.Application.Interface;
using SoftTrack.Domain;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _AccountService;
        public AccountController(IAccountService AccountService)
        {
            _AccountService = AccountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountAsync()
        {
            var ressult = await _AccountService.GetAllAccountAsync();
            return StatusCode(StatusCodes.Status200OK, ressult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync(AccountCreateDto AccountCreateDto)
        {
            await _AccountService.CreateAccountAsync(AccountCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccountAsync(AccountUpdateDto AccountUpdateDto)
        {
            await _AccountService.UpdateAccountAsync(AccountUpdateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccountAsync(AccountDto AccountDto)
        {
            await _AccountService.DeleteAccountAsync(AccountDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
