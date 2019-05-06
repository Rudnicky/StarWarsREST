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
    public class PlanetRepositoryTests
    {
        [Fact]
        public void GetPlanets_ShouldReturnPlanets_WhenBunchOfPlanetsWereAdded()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new PlanetRepository(context);

            // Act
            InitializePlanets(context);
            var result = repository.GetPlanets().ToList();

            // Assert
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void GetPlanet_ShouldReturnSpecificPlanet_AfterGivenIdOfPickedPlanet()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new PlanetRepository(context);

            // Act
            InitializePlanets(context);
            var result = repository.GetPlanet(1);

            // Assert
            Assert.Equal("Alderaan", result.PlanetName);
        }

        [Fact]
        public void AddPlanet_ShouldReturnSinglePlanet_WhenOnePlanetAdded()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new PlanetRepository(context);

            // Act
            repository.AddPlanet(new Planet() { PlanetName = "Alderaan" });
            var result = repository.GetPlanets().ToList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void UpdatePlanet_ShouldReturnDifferentPlanetName_WhenPlanetNameHasBeenModified()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new PlanetRepository(context);

            // Act
            InitializePlanets(context);
            var result = repository.GetPlanets().ToList();
            var originalPlanet = result.First();
            var originalPlanetName = originalPlanet.PlanetName;

            originalPlanet.PlanetName = "Nova";
            repository.UpdatePlanet(originalPlanet);

            var updatedPlanetName = repository.GetPlanet(1).PlanetName;

            // Assert
            Assert.NotEqual(originalPlanetName, updatedPlanetName);
        }

        [Fact]
        public void DeletePlanet_ShouldReturnDecrementedCounter_WhenOneOfThePlanetsWasRemoved()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsDb")
                .Options;

            var context = new StarWarsDbContext(options);
            var repository = new PlanetRepository(context);

            // Act
            InitializePlanets(context);
            var result = repository.GetPlanets().ToList();
            repository.DeletePlanet(result.First());
            var resultAfterDeletePerformed = repository.GetPlanets().ToList();

            // Assert
            Assert.Equal(resultAfterDeletePerformed.Count, result.Count - 1);
        }

        private void InitializePlanets(StarWarsDbContext context)
        {
            var planets = new Planet[]
            {
                new Planet() { PlanetName = "Alderaan" },
                new Planet() { PlanetName = "Naboo" },
                new Planet() { PlanetName = "Cirka" },
                new Planet() { PlanetName = "Earth" },
                new Planet() { PlanetName = "Mars" }
            };

            context.Planets.AddRange(planets);
            context.SaveChanges();
        }
    }
}
