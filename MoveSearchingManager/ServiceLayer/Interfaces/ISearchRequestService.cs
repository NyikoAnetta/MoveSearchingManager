using MovieSearchingManager.Models;
using System;
using System.Collections.Generic;

namespace MovieSearchingManager.ServiceLayer.Interfaces
{
    /// <summary>
    /// Handle searching requests service
    /// </summary>   
    public interface ISearchRequestService
    {
        /// <summary>
        /// Create SearchRequest entity
        /// </summary>
        /// <param name="title"></param>
        /// <param name="timer"></param>
        /// <param name="imdbId"></param>
        /// <returns>Created Search Request entity</returns>
        public SearchRequest CreateRequest(string title, double timer, string imdbId);

        /// <summary>
        /// Insert Search Request entity
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public void InsertRequest<T>(T record);

        /// <summary>
        /// Load all saved data
        /// </summary>
        /// <returns>List of T type data</returns>
        public List<T> LoadRequests<T>();

        /// <summary>
        /// Load saved data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T type data by id</returns>
        public T LoadRequestById<T>(Guid id);

        /// <summary>
        /// Load data from a given date range
        /// </summary>
        /// <param name="from">The start date of the date range</param>
        /// <param name="to">The end date of the date range</param>
        /// <returns>List of T type data</returns>
        public List<T> LoadRequestOnDatePeriod<T>(DateTime from, DateTime to);

        /// <summary>
        /// Report from the count of saved data grouped by day
        /// </summary>
        /// <returns>List of the reports</returns>
        public List<Report> ReportPerDay<T>();

        /// <summary>
        /// Delete entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success or error message</returns>
        public string DeleteRequestById<T>(Guid id);
    }
}
