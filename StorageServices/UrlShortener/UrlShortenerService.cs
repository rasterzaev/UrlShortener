using MongoDB.Driver;
using StorageServices.Contracts.UrlShortener;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageServices.UrlShortener
{
    public class UrlShortenerService : IUrlShortenerService
    {
        readonly MongoClient _client;
        readonly IMongoDatabase _dataBase;
        readonly IMongoCollection<UrlDocument> _urlCollection;

        const string _dataBaseName = "urlshortener";
        const string _collectionsName = "urls";

        public UrlShortenerService(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _dataBase = _client.GetDatabase(_dataBaseName);
            _urlCollection = _dataBase.GetCollection<UrlDocument>(_collectionsName);
        }

        public async Task<UrlDocument> GetByShortUrl(string shortUrl)
        {
            var query = await _urlCollection.FindAsync(x => x.ShortUrl == shortUrl);
            return await query.FirstOrDefaultAsync();
        }

        public async void SaveNewUrl(UrlDocument doc)
        {
            await _urlCollection.InsertOneAsync(doc);
        }

        public async Task<List<UrlDocument>> GetList(string sessionId)
        {
            var query = await _urlCollection.FindAsync(x => x.SessionId == sessionId);
            return await query.ToListAsync();
        }

        public async void IncrementCounter(string shortUrl)
        {
            var update = Builders<UrlDocument>.Update.Inc(r => r.Counter, 1);
            await _urlCollection.FindOneAndUpdateAsync(x => x.ShortUrl == shortUrl, update);
        }
    }
}