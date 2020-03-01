using BusinessLogic.Contracts.UrlShortener;
using BusinessLogics.UrlShortener.Helpers;
using StorageServices.Contracts.UrlShortener;
using System;
using System.Threading.Tasks;

namespace BusinessLogics.UrlShortener.RequestHandlers
{
    public class NewUrlRequestHandler
    {
        readonly IUrlShortenerService _shortenerService;

        public NewUrlRequestHandler(IUrlShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        public async Task<ShortenResponseContract> HandleAsync(ShortenRequestContract contract, string sessionId)
        {
            var shortUrl = ShortenUrlHelper.GetRandomString();
            while (await _shortenerService.GetByShortUrl(shortUrl) != null)
            {
                shortUrl = ShortenUrlHelper.GetRandomString();
            }

            var doc = new UrlDocument
            {
                Created = DateTime.UtcNow,
                SessionId = sessionId,
                LongUrl = contract.LongUrl,
                ShortUrl = shortUrl,
            };

            _shortenerService.SaveNewUrl(doc);

            return new ShortenResponseContract
            {
                ShortUrl = shortUrl,
            };
        }
    }
}
