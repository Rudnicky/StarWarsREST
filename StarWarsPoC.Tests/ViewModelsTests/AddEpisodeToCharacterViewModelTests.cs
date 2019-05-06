using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StarWarsPoC.Tests.ViewModelsTests
{
    /// <summary>
    /// Unit tests for the AddEpisodeToCharacterViewModel class
    /// Naming convention: MethodName_ExpectedBehavior_StateUnderTest
    /// </summary>
    public class AddEpisodeToCharacterViewModelTests
    {
        [Fact]
        public void Ctor_ShouldSetupCharacter_InitializedCharacter()
        {
            // Arrange
            var character = new Character() { Id = 1, Name = "Luke" };
            var episodes = new List<Episode>() { new Episode(), new Episode() };

            // Act
            var viewModel = new AddEpisodeToCharacterViewModel(character, episodes);
            var result = viewModel.Character;

            // Assert
            Assert.Equal(character, result);
        }

        [Fact]
        public void Ctor_ShouldSetupListOFEpisodes_InitializedEpisodes()
        {
            // Arrange
            var character = new Character() { Id = 1, Name = "Luke" };
            var episodes = new List<Episode>() { new Episode(), new Episode() };

            // Act
            var viewModel = new AddEpisodeToCharacterViewModel(character, episodes);
            var result = viewModel.Episodes;

            // Assert
            Assert.Equal(episodes.Count(), result.Count());
        }
    }
}
