using StarWarsPoC.Core.Domain;
using System.Linq;

namespace StarWarsPoC.Persistence.EntityConfigurations
{
    public class DbInitializer
    {
        public static void Initialize(StarWarsDbContext context)
        {
            // EnsureCreated will cause the database to be created
            // whenever it's needed to be. If it's already there
            // it won't do anything
            context.Database.EnsureCreated();

            // Check if specified table has any data in it
            // if not, then create some dummy data 
            if (context.Characters.Any())
            {
                return;
            }

            // Create loads of Dummy Data
            var characters = new Character[]
            {
                new Character() { Name = "Luke Skywalker" },
                new Character() { Name = "Darth Vader" },
                new Character() { Name = "Han Solo" },
                new Character() { Name = "Leia Organa" },
                new Character() { Name = "Wilhuff Tarkin" },
                new Character() { Name = "C-3PO" },
                new Character() { Name = "R2-D2" }
            };

            foreach (var character in characters)
            {
                context.Characters.Add(character);
            }
            context.SaveChanges();

            var episodes = new Episode[]
            {
                new Episode() { EpisodeName = "NEWHOPE" },
                new Episode() { EpisodeName = "EMPIRE" },
                new Episode() { EpisodeName = "JEDI" }
            };

            foreach (var episode in episodes)
            {
                context.Episodes.Add(episode);
            }
            context.SaveChanges();

            var planet = new Planet() { PlanetName = "Alderaan" };
            context.Planets.Add(planet);
            context.SaveChanges();

        }
    }
}
