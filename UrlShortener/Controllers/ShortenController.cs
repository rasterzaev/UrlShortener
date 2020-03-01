using BusinessLogic.Contracts.UrlShortener;
using BusinessLogics.UrlShortener.RequestHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageServices.Contracts.UrlShortener;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("shorten")]
    public class ShortenController : ControllerBase
    {
        private readonly NewUrlRequestHandler _newUrlHandler;
        private readonly GetListRequestHandler _getListHandler;

        public ShortenController(NewUrlRequestHandler newUrlHandler, GetListRequestHandler getListHandler)
        {
            _newUrlHandler = newUrlHandler;
            _getListHandler = getListHandler;
        }

        const string _sessionKey = "sessionId";

        [HttpPost]
        public async Task<ShortenResponseContract> Shorten([FromBody] ShortenRequestContract contract)
        {
            string sessionId = Request.Cookies[_sessionKey];

            if (string.IsNullOrWhiteSpace(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                Response.Cookies.Append(_sessionKey, sessionId, new CookieOptions
                {
                    Path = "/",
                    HttpOnly = false,
                    IsEssential = true,
                    Expires = DateTime.Now.AddYears(1),
                });
            }

            return await _newUrlHandler.HandleAsync(contract, sessionId);
        }

        [HttpGet]
        public async Task<List<UrlDocument>> GetList()
        {
            string sessionId = Request.Cookies[_sessionKey];

            return await _getListHandler.HandleAsync(sessionId);
        }
    }
}
