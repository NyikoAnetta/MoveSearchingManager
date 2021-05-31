using MovieSearchingManager.Models;
using MovieSearchingManager.ServiceLayer.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System;

namespace MovieSearchingManager.ServiceLayer.Implementation
{
    /// <summary>
    /// Implementaton of IMovieSearchingService interface
    /// </summary>
    public class MovieSearchingService : IMovieSearchingService
    {
        static string _address = "http://www.omdbapi.com/?apikey=511865fe&t=";
        public async Task<Movie> GetMovieByTitle(string title)
        {           
            var data = JsonConvert.DeserializeObject<Movie>(await GetDataFromServer(title));
            if (data.Title == null)
            {
                throw new ArgumentException("Movie not found!");
            }
            else
            {
                return data;
            }          
        }

        private async Task<string> GetDataFromServer(string title)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Path.Combine(_address, title));
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
