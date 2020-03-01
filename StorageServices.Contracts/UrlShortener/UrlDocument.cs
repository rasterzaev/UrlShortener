using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace StorageServices.Contracts.UrlShortener
{
    public class UrlDocument
    {
        [BsonId]
        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }

        [JsonIgnore]
        public string SessionId { get; set; }

        public DateTime Created { get; set; }

        public int Counter { get; set; }
    }
}
