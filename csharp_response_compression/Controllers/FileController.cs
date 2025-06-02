using csharp_response_compression.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csharp_response_compression.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetCompressedBase64")]
        public async Task<IActionResult> GetCompressedBase64()
        {
            try
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "72ae76e7-deb1-4a13-845c-dcdd47365e39_Webhook_based_job_scheduler.png");

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var memoryStream = new MemoryStream();
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                var fileBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);

                var files = new List<string>();
                for (int i = 0; i < 300; i++)
                {
                    files.Add(base64String);
                }

                return Ok(new FileModel()
                {
                    Files = files
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
