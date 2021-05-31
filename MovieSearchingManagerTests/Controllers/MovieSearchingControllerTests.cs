using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoveSearchingManager.Controllers;
using MovieSearchingManager.Controllers;
using MovieSearchingManager.Models;
using MovieSearchingManager.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearchingManager.Controllers.Tests
{
    [TestClass()]
    public class MovieSearchingControllerTests
    {
        [TestMethod()]
        public void WhenMovieDoesntExistsInDB_GetMovieByTitleMethod_ItShouldReturnException()
        {
            //arrange   
            var mockSearchRequestService = new Mock<ISearchRequestService>();
            var mockMovieSearchingService = new Mock<IMovieSearchingService>();
            mockMovieSearchingService.Setup(s => s.GetMovieByTitle("test")).Throws(new ArgumentException("Movie not found!"));
            var _controller = new MovieSearchingController(mockMovieSearchingService.Object, mockSearchRequestService.Object);

            //act

            //assert
            Assert.ThrowsException<ArgumentException>(() => _controller.GetMovieByTitle("test"));
        }

        [TestMethod()]
        public void WhenMethodGetEmptyTitle_GetMovieByTitleMethod_ItShouldReturnException()
        {
            //arrange   
            var mockSearchRequestService = new Mock<ISearchRequestService>();
            var mockMovieSearchingService = new Mock<IMovieSearchingService>();
            var _controller = new MovieSearchingController(mockMovieSearchingService.Object, mockSearchRequestService.Object);

            //act

            //assert
            Assert.ThrowsException<ArgumentException>(() => _controller.GetMovieByTitle(""));
        }

        [TestMethod()]
        public void WhenMovieExistsInDB_GetMovieByTitleMethod_ItShouldReturnMovieEntity()
        {
            //arrange   
            var mockSearchRequestService = new Mock<ISearchRequestService>();
            var mockMovieSearchingService = new Mock<IMovieSearchingService>();
            var expectedMovieObject = new Movie()
            {
                Actors = "Leonardo DiCaprio, Kate Winslet, Billy Zane, Kathy Bates",
                Awards = "Won 11 Oscars. Another 115 wins & 83 nominations.",
                Country = "USA, Mexico, Australia, Canada",
                Director = "James Cameron",
                Genre = "Drama, Romance",
                ImdbId = "tt0120338",
                ImdbRating = "7.8",
                ImdbVotes = "1,067,012",
                Language = "English, Swedish, Italian, French",
                Plot = "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.",
                Poster = "https://m.media-amazon.com/images/M/MV5BMDdmZGU3NDQtY2E5My00ZTliLWIzOTUtMTY4ZGI1YjdiNjk3XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_SX300.jpg",
                Rated = "PG-13",
                Released = "19 Dec 1997",
                Runtime = "194 min",
                Title = "Titanic",
                Writer = "James Cameron",
                Year = 1997
            };
            var expectedRequestObject = new SearchRequest()
            {
                ImdbId = "tt0120338",
                IpAddress = "fe80::ecb4:2451:7e23:1fbe%19",
                SearchToken = "titanic",
                TimeMs = 5030.6263,
                TimeStamp = new DateTime()
            };
            mockMovieSearchingService.Setup(s => s.GetMovieByTitle("titanic")).Returns(Task.FromResult(expectedMovieObject));
            mockSearchRequestService.Setup(s => s.CreateRequest("titanic", It.IsAny<double>(), "tt0120338")).Returns(expectedRequestObject);
            var _controller = new MovieSearchingController(mockMovieSearchingService.Object, mockSearchRequestService.Object);

            //act
            var sut = _controller.GetMovieByTitle("titanic");

            //assert
            Assert.AreEqual(sut, expectedRequestObject);
        }
    }
}