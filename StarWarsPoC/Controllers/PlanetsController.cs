using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using System.Linq;

namespace StarWarsPoC.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly IPlanetRepository _planetRepository;

        public PlanetsController(IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        // GET: Planets
        public IActionResult Index()
        {
            return View(_planetRepository.GetPlanets().ToList());
        }

        // GET: Planets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int planetId = (int)id;
            var planet = _planetRepository.GetPlanet(planetId);
            if (planet == null)
            {
                return NotFound();
            }

            return View(planet);
        }

        // GET: Planets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PlanetId,PlanetName")] Planet planet)
        {
            if (ModelState.IsValid)
            {
                _planetRepository.AddPlanet(planet);
                return RedirectToAction(nameof(Index));
            }
            return View(planet);
        }

        // GET: Planets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int planetId = (int)id;
            var planet = _planetRepository.GetPlanet(planetId);
            if (planet == null)
            {
                return NotFound();
            }
            return View(planet);
        }

        // POST: Planets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PlanetId,PlanetName")] Planet planet)
        {
            if (id != planet.PlanetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _planetRepository.UpdatePlanet(planet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetExists(planet.PlanetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(planet);
        }

        // GET: Planets/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int planetId = (int)id;
            var planet = _planetRepository.GetPlanet(planetId);
            if (planet == null)
            {
                return NotFound();
            }

            return View(planet);
        }

        // POST: Planets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var planet = _planetRepository.GetPlanet(id);
            if (planet != null)
            {
                _planetRepository.DeletePlanet(planet);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetExists(int id)
        {
            return _planetRepository.GetPlanets().Any(e => e.PlanetId == id);
        }
    }
}
