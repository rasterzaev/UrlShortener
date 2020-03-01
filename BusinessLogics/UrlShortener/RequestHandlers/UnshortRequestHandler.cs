using StorageServices.Contracts.UrlShortener;
using System.Threading.Tasks;

namespace BusinessLogics.UrlShortener.RequestHandlers
{
    public class UnshortRequestHandler
    {
        readonly IUrlShortenerService _shortenerService;

        public UnshortRequestHandler(IUrlShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        public async Task<string> HandleAsync(string shortUrl)
        {
            var doc = await _shortenerService.GetByShortUrl(shortUrl);

            if (doc != null)
            {
                _shortenerService.IncrementCounter(shortUrl);
            }

            return doc?.LongUrl;
        }
    }
}
