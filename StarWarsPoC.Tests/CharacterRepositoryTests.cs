using Microsoft.EntityFrameworkCore;
using StarWarsPoC.Core.Domain;
using StarWarsPoC.Persistence.EntityConfigurations;
using StarWarsPoC.Persistence.Repositories;
using System.Linq;
using Xunit;

namespace StarWarsPoC.Tests
{
    /// <summary>
    /// Unit tests for the PlanetRepository class
    /// Naming convention: MethodName_ExpectedBehavior_StateUnderTest
    /// </summary>
    public class CharacterRepositoryTests
    {
        [Fact]
        public void GetCharacters_ShouldReturnCharacters_WhenBunchOfCharactersWereAdded()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new CharacterRepository(context);

            // Act
            InitializeCharacters(context);
            var result = repository.GetCharacters().ToList();

            // Assert
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void GetCharacter_ShouldReturnSpecificCharacter_AfterGivenIdOfPickedCharacter()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new CharacterRepository(context);

            // Act
            InitializeCharacters(context);
            var result = repository.GetCharacter(1);

            // Assert
            Assert.Equal("Luke", result.Name);
        }

        [Fact]
        public void AddCharacter_ShouldReturnSingleCharacter_WhenOneCharacterAdded()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new CharacterRepository(context);

            // Act
            repository.AddCharacter(new Character() { Name = "Luke Skywalker" });
            var result = repository.GetCharacters().ToList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void UpdateCharacter_ShouldReturnDifferentCharacterName_WhenCharacterNameHasBeenModified()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new CharacterRepository(context);

            // Act
            InitializeCharacters(context);
            var result = repository.GetCharacters().ToList();
            var originalCharacter = result.First();
            var originalCharacterName = originalCharacter.Name;

            originalCharacter.Name = "Duku";
            repository.UpdateCharacter(originalCharacter);

            var updatedCharacterName = repository.GetCharacter(1).Name;

            // Assert
            Assert.NotEqual(originalCharacterName, updatedCharacterName);
        }

        [Fact]
        public void DeleteCharacter_ShouldReturnDecrementedCounter_WhenOneOfTheCharactersWereRemoved()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new CharacterRepository(context);

            // Act
            InitializeCharacters(context);
            var result = repository.GetCharacters().ToList();
            repository.DeleteCharacter(result.First());
            var resultAfterDeletePerformed = repository.GetCharacters().ToList();

            // Assert
            Assert.Equal(resultAfterDeletePerformed.Count, result.Count - 1);
        }

        private void InitializeCharacters(StarWarsDbContext context)
        {
            var characters = new Character[]
            {
                new Character() { Name = "Luke" },
                new Character() { Name = "Leia" },
                new Character() { Name = "Vader" },
                new Character() { Name = "Obiwan" },
                new Character() { Name = "yoda" }
            };

            context.Characters.AddRange(characters);
            context.SaveChanges();
        }
    }
}
