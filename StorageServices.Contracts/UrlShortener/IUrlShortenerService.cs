using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageServices.Contracts.UrlShortener
{
    public interface IUrlShortenerService
    {
        Task<UrlDocument> GetByShortUrl(string shortUrl);

        void SaveNewUrl(UrlDocument doc);

        Task<List<UrlDocument>> GetList(string sessionId);

        void IncrementCounter(string shortUrl);
    }
}
