using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly soft_track5Context _context;
        private readonly IConfiguration _configuration;
        public ImageController(IConfiguration configuration, soft_track5Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpGet("ListImages")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> ListAllImagesAsync()
        {
            var lst = await _context.Images
                .Select(item => new ImageDto
                {
                    ImageId = item.ImageId,
                    ReportId = item.ReportId,
                    Image1 = item.Image1,
                })
                .ToListAsync();

            return lst;
        }

        [HttpGet("list_Images_by_Report/{key}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesByReportAsync(int key)
        {
            var lst = await _context.Images
                .Where(item => item.ReportId == key) 
                .Select(item => new ImageDto
                {
                    ImageId = item.ImageId,
                    ReportId = item.ReportId,
                    Image1 = item.Image1
                })
                .ToListAsync();

            if (!lst.Any())
            {
                return NotFound();
            }

            return lst;
        }
        [HttpPost("CreateImage")]
        public async Task<IActionResult> CreateImageAsync([FromBody] ImageCreateDto item)
        {
            if (ModelState.IsValid)
            {
                var tmp = new Image
                {
                    ReportId = item.ReportId,
                    Image1 = item.Image1
                };
                
                _context.Images.Add(tmp);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetImage", new { id = tmp.ImageId }, tmp);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut("UpdateImage/{key}")]
        public async Task<IActionResult> UpdateImageAsync(int key, [FromBody] ImageUpdateDto updatedImageDto)
        {
            if (updatedImageDto == null)
            {
                return BadRequest(ModelState);
            }
            var updatedImage = await _context.Images.FindAsync(key);

            if (updatedImage == null)
            {
                return NotFound("Image not found");
            }

            if (updatedImageDto.ReportId != 0)
            {
                updatedImage.ReportId = updatedImageDto.ReportId;
            }

            if (updatedImageDto.Image1 != null && updatedImageDto.Image1 != "string")
            {
                updatedImage.Image1 = updatedImageDto.Image1;
            }
            
            await _context.SaveChangesAsync();

            return Ok("Image updated successfully");
        }
        [HttpDelete("DeleteImageWith_key")]
        public async Task<IActionResult> DeleteImageAsync(int imageId)
        {
            var item = await _context.Images.FindAsync(imageId);

            if (item != null)
            {
                _context.Images.Remove(item);
                await _context.SaveChangesAsync();
            }
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
