using StorageServices.Contracts.UrlShortener;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogics.UrlShortener.RequestHandlers
{
    public class GetListRequestHandler
    {
        readonly IUrlShortenerService _shortenerService;

        public GetListRequestHandler(IUrlShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        public async Task<List<UrlDocument>> HandleAsync(string sessionId)
        {
            return await _shortenerService.GetList(sessionId);
        }
    }
}
