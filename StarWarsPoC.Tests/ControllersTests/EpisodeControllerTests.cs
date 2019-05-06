using Microsoft.AspNetCore.Mvc;
using Moq;
using StarWarsPoC.Controllers;
using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StarWarsPoC.Tests.ControllersTests
{
    /// <summary>
    /// Unit tests for the EpisodeController class
    /// Naming convention: MethodName_ExpectedBehavior_StateUnderTest
    /// </summary>
    public class EpisodeControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfEpisodes()
        {
            // Arrange
            var mockRepo = new Mock<IEpisodeRepository>();
            mockRepo.Setup(repo => repo.GetEpisodes())
                .Returns(GetEpisodes());
            var controller = new EpisodesController(mockRepo.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Episode>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void Details_ReturnsAViewResult_SpecifiedEpisode()
        {
            // Arrange
            var mockRepo = new Mock<IEpisodeRepository>();
            mockRepo.Setup(repo => repo.GetEpisodes())
                .Returns(GetEpisodes());
            var controller = new EpisodesController(mockRepo.Object);

            // Act
            var result = controller.Details(3);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Episode>(
                viewResult.ViewData.Model);
            Assert.Equal("JEDI", model.EpisodeName);
        }

        [Fact]
        public void Edit_ReturnsNotFoundActionResult_NotExisitingId()
        {
            // Arrange
            var mockRepo = new Mock<IEpisodeRepository>();
            mockRepo.Setup(repo => repo.GetEpisodes())
                .Returns(GetEpisodes());
            var controller = new EpisodesController(mockRepo.Object);

            // Act
            var result = (NotFoundResult)controller.Edit(666);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void DeleteConfirmed_RedirectToIndex_SpecifiedEpisode()
        {
            // Arrange
            var mockRepo = new Mock<IEpisodeRepository>();
            mockRepo.Setup(repo => repo.GetEpisodes())
                .Returns(GetEpisodes());
            var controller = new EpisodesController(mockRepo.Object);

            // Act
            var result = controller.DeleteConfirmed(2);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }

        private List<Episode> GetEpisodes()
        {
            var episodes = new List<Episode>();
            episodes.Add(new Episode()
            {
                EpisodeId = 1,
                EpisodeName = "NEWHOPE"
            });
            episodes.Add(new Episode()
            {
                EpisodeId = 2,
                EpisodeName = "EMPIRE"
            });
            episodes.Add(new Episode()
            {
                EpisodeId = 3,
                EpisodeName = "JEDI"
            });
            return episodes;
        }
    }
}
