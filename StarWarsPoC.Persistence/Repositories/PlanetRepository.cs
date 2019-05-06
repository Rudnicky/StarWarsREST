using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using StarWarsPoC.Persistence.EntityConfigurations;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsPoC.Persistence.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly StarWarsDbContext _context;

        public PlanetRepository(StarWarsDbContext context)
        {
            _context = context;
        }

        public void AddPlanet(Planet planet)
        {
            _context.Planets.Add(planet);
            _context.SaveChanges();
        }

        public void DeletePlanet(Planet planet)
        {
            _context.Planets.Remove(planet);
            _context.SaveChanges();
        }

        public Planet GetPlanet(int id)
        {
            return _context.Planets.Single(c => c.PlanetId == id);
        }

        public IEnumerable<Planet> GetPlanets()
        {
            return _context.Planets;
        }

        public void UpdatePlanet(Planet planet)
        {
            _context.Planets.Update(planet);
            _context.SaveChanges();
        }
    }
}
