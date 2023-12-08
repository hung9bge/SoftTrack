using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using System.Globalization;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : Controller
    {
        private readonly soft_track5Context _context;
        private readonly IConfiguration _configuration;

        public LibraryController(IConfiguration configuration, soft_track5Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("ListLibraries")]
        public async Task<ActionResult<IEnumerable<LibraryDto>>> ListAllLibrariesAsync()
        {
            var libraryDtos = await _context.Libraries
                 .OrderBy(library => library.Status)
                .OrderBy(library => library.StartDate)
                .Select(library => new LibraryDto
                {
                    LibraryId = library.LibraryId,
                    AppId = library.AppId,
                    Name = library.Name,
                    Publisher = library.Publisher,
                    LibraryKey = library.LibraryKey,
                    Start_Date = library.StartDate.ToString("dd/MM/yyyy"),
                    Time = library.Time,
                    Status = library.Status
                })
                .ToListAsync();

            return libraryDtos;
        }

        [HttpPost("CreateLibrary")]
        public async Task<IActionResult> CreateLibraryAsync([FromBody] LibraryCreateDto libraryDto)
        {
            if (ModelState.IsValid)
            {
                var library = new Library
                {
                    AppId = libraryDto.AppId,
                    Name = libraryDto.Name,
                    Publisher = libraryDto.Publisher,
                    LibraryKey = libraryDto.LibraryKey,
                    Time = libraryDto.Time,
                    Status = libraryDto.Status
                };
                if (DateTime.TryParseExact(libraryDto.Start_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    library.StartDate = parsedDate;
                }
 
                _context.Libraries.Add(library);
                await _context.SaveChangesAsync();

                return CreatedAtAction("CreateLibrary", new { id = library.LibraryId }, library);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("UpdateLibrary/{id}")]
        public async Task<IActionResult> UpdateLibraryAsync(int id, [FromBody] LibraryUpdateDto updatedLibraryDto)
        {
            var updatedLibrary = await _context.Libraries.FindAsync(id);

            if (updatedLibrary == null)
            {
                // Xử lý khi không tìm thấy thư viện
                return NotFound("Library not found");
            }

            // Cập nhật các trường cần thiết của thư viện
            if (updatedLibraryDto.Name != null && updatedLibraryDto.Name != "string")
            {
                updatedLibrary.Name = updatedLibraryDto.Name;
            }

            if (updatedLibraryDto.Publisher != null && updatedLibraryDto.Publisher != "string")
            {
                updatedLibrary.Publisher = updatedLibraryDto.Publisher;
            }

            if (updatedLibraryDto.LibraryKey != null && updatedLibraryDto.LibraryKey != "string")
            {
                updatedLibrary.LibraryKey = updatedLibraryDto.LibraryKey;
            }

            if (updatedLibraryDto.Start_Date != "string")
            {
                updatedLibrary.StartDate = DateTime.Parse(updatedLibraryDto.Start_Date);
            }
        
                updatedLibrary.Time = updatedLibraryDto.Time;
            

            if (updatedLibraryDto.Status != 0)
            {
                updatedLibrary.Status = updatedLibraryDto.Status;
            }

            // Gán các giá trị khác tương ứng từ updatedLibraryDto
            _context.Libraries.Update(updatedLibrary);
            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("ListLibrariesByApp/{appId}")]
        public async Task<ActionResult<IEnumerable<LibraryDto>>> ListLibrariesByAppAsync(int appId)
        {
            var libraryDtos = await _context.Libraries
                .Where(library => library.AppId == appId)
                .OrderBy(library => library.Status)
                .OrderBy(library => library.StartDate)
                .Select(library => new LibraryDto
                {
                    LibraryId = library.LibraryId,
                    AppId = library.AppId,
                    Name = library.Name,
                    Publisher = library.Publisher,
                    LibraryKey = library.LibraryKey,
                    Start_Date = library.StartDate.ToString("dd/MM/yyyy"),
                    Time = library.Time,
                    Status = library.Status
                })
                .ToListAsync();

            if (!libraryDtos.Any())
            {
                return NotFound();
            }

            return libraryDtos;
        }


        //[HttpDelete("DeleteLibraryWith_key")]
        //public async Task<IActionResult> DeleteLibraryAsync(int libraryId)
        //{
        //    var library = await _context.Libraries.FindAsync(libraryId);

        //    if (library != null)
        //    {
        //        library.Status = 3;

        //        await _context.SaveChangesAsync();
        //    }

        //    return Ok("Library has been deleted successfully.");
        //}
    }

}
