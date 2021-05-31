using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MovieSearchingManager.Attributes;
using MovieSearchingManager.Models;
using MovieSearchingManager.ServiceLayer.Interfaces;

namespace MovieSearchingManager.Controllers
{
    /// <summary>
    /// The Admin Controller is responsible for various searching requests handling .
    /// </summary>
    [ApiKey]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly ISearchRequestService _searchRequestService;

        /// <summary>
        /// Admind controller constructor 
        /// </summary>
        /// <param name="searchRequestService">searchRequestService initialization</param>
        public AdminController(ISearchRequestService searchRequestService)
        {
            _searchRequestService = searchRequestService;
        }

        /// <summary>
        /// Get all stored request entries  
        /// </summary>
        /// <returns>List of saved search requests</returns>
        [HttpGet]
        public List<SearchRequest> GetAllRequests()
        {
            return _searchRequestService.LoadRequests<SearchRequest>();
        }

        /// <summary>
        /// Get a single request entry by Guid id
        /// </summary>
        /// <param name="id">data from the user</param>
        /// <returns>A search request entity o exception</returns>
        [HttpGet]
        public SearchRequest GetRequestById(Guid id)
        {
            if (id != null)
            {
                return _searchRequestService.LoadRequestById<SearchRequest>(id);
            } else
            {
                throw new ArgumentException("Please give id!");
            }
        }

        /// <summary>
        /// Search saved requests on date period
        /// </summary>
        /// <param name="from">The start date of the date range</param>
        /// <param name="to">The end date of the date range</param>
        /// <returns>List of search requests or Exception</returns>
        [HttpGet]
        public List<SearchRequest> GetOnDatePeriod(DateTime from, DateTime to)
        {
            if (from != null && to != null)
            {
                return _searchRequestService.LoadRequestOnDatePeriod<SearchRequest>(from, to);

            } else
            {
                throw new ArgumentException("Please give from and to dates!");
            }
        }

        /// <summary>
        /// Report usage on per day (DD-MM-YYYY) - An overview on the number of requests based on an timestamp (to construct this data use a MongoDB aggregation)
        /// </summary>
        /// <returns>List of Reports</returns>
        [HttpGet]
        public List<Report> ReportPerDay()
        {
            return _searchRequestService.ReportPerDay<BsonDocument>();
        }

        /// <summary>
        /// Delete request entry
        /// </summary>
        /// <param name="id">data from the user</param>
        /// <returns>A string success, error message or exception</returns>
        [HttpDelete]
        public string DeleteRequestById(Guid id)
        {
            if (id != null)
            {
                return _searchRequestService.DeleteRequestById<SearchRequest>(id);
            }
            else
            {
                throw new ArgumentException("Please give id!");
            }
        }
    }
}
