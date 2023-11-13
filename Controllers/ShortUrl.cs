using cutli_backend.Data;
using cutli_backend.Models;
using cutli_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cutli_backend.Controllers;

[ApiController]
public class ShortUrlController : ControllerBase
{
    private readonly IShortUrlServices _shortUrlServices;

    public ShortUrlController(IShortUrlServices shortUrlServices)
    {
        _shortUrlServices = shortUrlServices;
    }

    public class UrlRequest
    {
        public string? OriginalUrl { get; set; }
    }

    [HttpPost("api/shorturl")]
    public async Task<IActionResult> ShortUrl([FromBody] UrlRequest url)
    {
        if (url.OriginalUrl == null)
            return BadRequest("Url is null");
        if (!new ShortUrlUtils().IsUrlValid(url.OriginalUrl))
            return BadRequest("Url is not valid");
        string result = await _shortUrlServices.ShortUrl(url.OriginalUrl);
        return Ok(result);
    }

    [HttpGet("{shortUrl}")]	
    public async Task<IActionResult> RedirectUrl(string shortUrl)
    {
        var url = await _shortUrlServices.GetUrl(shortUrl);
        if (url == null || url.OriginalUrl == null)
            return NotFound();
        return Redirect(url.OriginalUrl);
    }
}
