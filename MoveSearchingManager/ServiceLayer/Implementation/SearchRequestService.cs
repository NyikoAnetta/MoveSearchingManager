using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MovieSearchingManager.Models;
using MovieSearchingManager.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MovieSearchingManager.ServiceLayer.Implementation
{
    /// <summary>
    /// Implementation of ISearchRequestService interface
    /// </summary>
    public class SearchRequestService : ISearchRequestService
    {
        private IMongoDatabase _db;
        private readonly string _table;
        public SearchRequestService()
        {
            var client = new MongoClient();
            _db = client.GetDatabase("MovieSearching");
            _table = "SearchRequests";
        }

        public SearchRequest CreateRequest(string title, double timer, string imdbId)
        {
            string myIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            SearchRequest searchRequest = new SearchRequest()
            {
                SearchToken = title,
                ImdbId = imdbId,
                TimeMs = timer,
                TimeStamp = DateTime.Now,
                IpAddress = myIP
            };
            return searchRequest;
        }

        public void InsertRequest<T>(T record)
        {
            var collection = _db.GetCollection<T>(_table);
            collection.InsertOne(record);
        }

        public List<T> LoadRequests<T>()
        {
            var collection = _db.GetCollection<T>(_table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRequestById<T>( Guid id)
        {
            var collection = _db.GetCollection<T>(_table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public List<T> LoadRequestOnDatePeriod<T>(DateTime from, DateTime to) 
        {
            var collection = _db.GetCollection<T>(_table);
            to = to.Date + new TimeSpan(23, 59, 59); 
            var filter = Builders<T>.Filter.Gte("TimeStamp", from) & Builders<T>.Filter.Lte("TimeStamp", to);
            return collection.Find(filter).ToList();
        }

        public string DeleteRequestById<T>(Guid id)
        {
            var collection = _db.GetCollection<T>(_table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
            return "Successfully deleted";
        }

        public List<Report> ReportPerDay<T>()
        {
            var collection = _db.GetCollection<SearchRequest>(_table);
            var result = collection
                            .Aggregate()
                            .Group(new BsonDocument { 
                                { "_id", new BsonDocument { { "month", new BsonDocument("$month", "$timestamp") },
                                    { "day", new BsonDocument("$dayOfMonth", "$timestamp") },
                                    { "year", new BsonDocument("$year", "$timestamp") } }
                                },
                                { "report_count", new BsonDocument("$sum", 1) }
                            })
                            .ToList();
            var reportJson = new List<Report>();
            foreach(var test in result)
            {
                reportJson.Add(BsonSerializer.Deserialize<Report>(test));
            }
            
            return reportJson;
        }
    }
}
