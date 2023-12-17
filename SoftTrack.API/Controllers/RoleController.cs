
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;


namespace SoftTrack.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly soft_track5Context _context;
        private readonly IConfiguration _configuration;

        public RoleController(soft_track5Context context)
        {
            _context = context;
        }

        public interface IRoleController
        {
            Task<IEnumerable<Role>> GetRoles();
        }

        // GET: api/roles
        [HttpGet("listRole")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _context.Roles        
                .ToListAsync();
            if (!roles.Any())
            {
                return NotFound();
            }
            return Ok(roles);
        }
    }
}


