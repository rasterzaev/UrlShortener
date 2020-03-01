using System.IO;

namespace BusinessLogics.UrlShortener.Helpers
{
    public class ShortenUrlHelper
    {
        public static string GetRandomString()
        {
            return Path.GetRandomFileName().Substring(0, 7);
        }
    }
}
