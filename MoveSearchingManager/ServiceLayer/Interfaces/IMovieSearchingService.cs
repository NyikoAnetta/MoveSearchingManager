using MovieSearchingManager.Models;
using System.Threading.Tasks;

namespace MovieSearchingManager.ServiceLayer.Interfaces
{
    /// <summary>
    /// Handle data from Open Movie Database service
    /// </summary>
    public interface IMovieSearchingService
    {
        /// <summary>
        /// Get movie by title from Open Movie Database
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Movie type data by title</returns>
        Task<Movie> GetMovieByTitle(string title);
    }
}
