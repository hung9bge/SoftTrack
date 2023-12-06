
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

        public RoleController(soft_track5Context context)
        {
            _context = context;  
        }

        // GET: api/roles
        [HttpGet("listRole")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _context.Roles        
                .ToListAsync();
            return Ok(roles);
        }
    }
}


