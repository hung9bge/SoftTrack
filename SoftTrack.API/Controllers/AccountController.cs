using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Application.DTO;
using SoftTrack.Application.Interface;
using SoftTrack.Domain;
using System.Data;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly soft_trackContext _context;

        public AccountController(soft_trackContext context)
        {
            _context = context;
        }

        [HttpGet("admin-action")]
        [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin" truy cập.
        public IActionResult AdminAction()
        {
            return Ok("Admin action");
        }

        [HttpGet("user-action")]
        [Authorize(Roles = "User")] // Chỉ cho phép người dùng có vai trò "User" truy cập.
        public IActionResult UserAction()
        {
            return Ok("User action");
        }

        [HttpGet("guest-action")]
        [Authorize(Roles = "Guest")] // Chỉ cho phép người dùng có vai trò "Guest" truy cập.
        public IActionResult GuestAction()
        {
            return Ok("Guest action");
        }
    }
}
