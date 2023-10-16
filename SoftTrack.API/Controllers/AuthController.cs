using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SoftTrack.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("google")]
        public IActionResult GoogleLogin()
        {
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            return Challenge(authProperties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                // Xử lý thông tin người dùng sau khi đăng nhập thành công bằng Google
                // Thường sẽ có thông tin trong result.Principal
                return Ok(result.Principal);
            }
            else
            {
                // Xử lý khi đăng nhập thất bại
                return Unauthorized();
            }
        }
        

    }

}
