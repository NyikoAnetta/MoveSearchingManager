using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MovieSearchingManager.Models
{
    public class SearchRequest
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("search_token")]
        public string SearchToken { get; set; }
        [BsonElement("imdbID")]
        public string ImdbId { get; set; }
        [BsonElement("processing_time_ms")]
        public double TimeMs { get; set; }
        [BsonElement("timestamp")]
        public DateTime TimeStamp { get; set; }
        [BsonElement("ip_address")]
        public string IpAddress { get; set; }
    }
}
