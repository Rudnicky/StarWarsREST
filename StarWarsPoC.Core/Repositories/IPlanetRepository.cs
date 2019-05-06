using StarWarsPoC.Core.Domain;
using System.Collections.Generic;

namespace StarWarsPoC.Core.Repositories
{
    public interface IPlanetRepository
    {
        IEnumerable<Planet> GetPlanets();
        Planet GetPlanet(int id);
        void AddPlanet(Planet planet);
        void UpdatePlanet(Planet planet);
        void DeletePlanet(Planet planet);
    }
}
