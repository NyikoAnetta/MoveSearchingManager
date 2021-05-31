using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSearchingManager.Models
{
    public class Report
    {
        [BsonElement("_id")]
        public ReportId ReportId { get; set; }
        [BsonElement("report_count")]
        public int ReportCount { get; set; }
    }

    public class ReportId
    {
        [BsonElement("year")]
        public int Year { get; set; }
        [BsonElement("month")]
        public int Month { get; set; }
        [BsonElement("day")]
        public int Day { get; set; }
    }
}
