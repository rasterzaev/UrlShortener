using BusinessLogics.UrlShortener.RequestHandlers;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("")]
    public class UnshortController : ControllerBase
    {
        private readonly UnshortRequestHandler _unshortHandler;

        public UnshortController(UnshortRequestHandler unshortHandler)
        {
            _unshortHandler = unshortHandler;
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public IActionResult Unshort(string shortUrl)
        {
            var longUrl = _unshortHandler.HandleAsync(shortUrl).Result;

            if (!string.IsNullOrWhiteSpace(longUrl))
            {
                return RedirectPermanent(longUrl);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
