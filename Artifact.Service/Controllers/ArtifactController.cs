using Artifact.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;



namespace Artifact.Service.Controllers;

[ApiController]
[Route("/")]
public class ArtifactController(IStorageService storageService) : ControllerBase
{
    private readonly IStorageService _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

    [HttpGet("/")]
    public IActionResult Get()
    {
        var host = HttpContext.Request.Host.Value;
        return Ok($"Artifact Service is running on {host}");
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadAsync([FromForm] string name, IFormFile file)
    {
        using var stream = file.OpenReadStream();
        await _storageService.UploadAsync(name, stream);
        return Ok("File uploaded successfully.");
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadAsync([FromQuery] string name)
    {
        Stream stream = await _storageService.DownloadAsync(name);
        return File(stream, "application/octet-stream", name);
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListAsync()
    {
        List<string> items = await _storageService.ListAsync();
        return Ok(items);
    }


}
