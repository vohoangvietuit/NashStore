using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[Route("api/upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public UploadController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost("image")]
    [Authorize]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile photo)
    {
        if (photo == null || photo.Length == 0)
            return BadRequest(new { message = "No file uploaded" });

        // Validate file type
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var fileExtension = Path.GetExtension(photo.FileName).ToLowerInvariant();
        
        if (!allowedExtensions.Contains(fileExtension))
            return BadRequest(new { message = "Invalid file type. Only JPG, PNG, and GIF files are allowed." });

        // Generate unique filename
        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        var folderName = Path.Combine("wwwroot", "uploads");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        // Create directory if it doesn't exist
        if (!Directory.Exists(pathToSave))
            Directory.CreateDirectory(pathToSave);

        var fullPath = Path.Combine(pathToSave, fileName);
        var relativePath = Path.Combine("uploads", fileName).Replace("\\", "/");

        try
        {
            using var stream = new FileStream(fullPath, FileMode.Create);
            await photo.CopyToAsync(stream);

            return Ok(new { 
                message = "File uploaded successfully", 
                fileName = fileName,
                path = $"/{relativePath}"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"File upload failed: {ex.Message}" });
        }
    }
}
