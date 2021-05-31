using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieSearchingManager.ServiceLayer.Interfaces;
using MovieSearchingManager.Models;
using System.Diagnostics;
using System;

namespace MoveSearchingManager.Controllers
{
    /// <summary>
    /// The Movie Searching Controller is responsible for collecting data from omdbapi Open Moview Database.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class MovieSearchingController : ControllerBase
    {
        private readonly IMovieSearchingService _movieSearhingService;
        private readonly ISearchRequestService _searchRequestService;

        /// <summary>
        /// Movie searching controller constructor
        /// </summary>
        /// <param name="movieSearchingService">movieSearchingService initialization</param>
        /// <param name="searchRequestService">searchRequestService initialization</param>
        public MovieSearchingController(IMovieSearchingService movieSearchingService, ISearchRequestService searchRequestService)
        {
            _movieSearhingService = movieSearchingService;
            _searchRequestService = searchRequestService;
        }

        /// <summary>
        /// Get movie by title from Open Movie Database, create and insert Search Request entity
        /// </summary>
        /// <param name="title">data from the user</param>
        /// <returns>A search request entity or Exception</returns>
        [HttpGet]
        public SearchRequest GetMovieByTitle(string title)
        {
            if (!String.IsNullOrEmpty(title))
            {
                try
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    var movieByTitle = _movieSearhingService.GetMovieByTitle(title).Result;
                    timer.Stop();
                    var recordToInsert = _searchRequestService.CreateRequest(title, timer.Elapsed.TotalMilliseconds, movieByTitle.ImdbId);
                    _searchRequestService.InsertRequest(recordToInsert);
                    return recordToInsert;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Movie not found!");
                }
            }
            else
            {
                throw new ArgumentException("Please give title!");
            }
        }
    }
}
