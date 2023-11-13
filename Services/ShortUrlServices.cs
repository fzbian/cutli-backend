using cutli_backend.Data;
using cutli_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace cutli_backend.Services
{
    public class ShortUrlUtils
    {
        public string GenerateRandomCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
                stringChars[i] = chars[random.Next(chars.Length)];

            return new string(stringChars);
        }

        public bool IsUrlValid(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult))
                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            else
                return false;
        }
    }

    public interface IShortUrlServices
    {
        Task<string> ShortUrl(string originalUrl);
        Task<Url?> GetUrl(string shortUrl);
    }

    public class ShortUrlServices : IShortUrlServices
    {
        private readonly AppDbContext _context;

        public ShortUrlServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> ShortUrl(string originalUrl)
        {
            var url = new Url
            {
                OriginalUrl = originalUrl,
                ShortUrl = new ShortUrlUtils().GenerateRandomCode()
            };
            _context.Urls.Add(url);
            await _context.SaveChangesAsync();
            return url.ShortUrl;
        }

        public async Task<Url?> GetUrl(string shortUrl)
        {
            try
            {
                var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
                return url;
            }
            catch 
            {
                return null;
            }
        }
    }
}