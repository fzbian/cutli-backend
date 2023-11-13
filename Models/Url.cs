using cutli_backend.Services;

namespace cutli_backend.Models
{
    public class Url
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? OriginalUrl { get; set; }
        public string? ShortUrl { get; set; } = new ShortUrlUtils().GenerateRandomCode();
    }
}