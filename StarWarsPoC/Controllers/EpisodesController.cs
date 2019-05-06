using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using System.Linq;

namespace StarWarsPoC.Controllers
{
    public class EpisodesController : Controller
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodesController(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        // GET: Episodes
        public IActionResult Index()
        {
            return View(_episodeRepository.GetEpisodes().ToList());
        }

        // GET: Episodes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodeId = (int)id;
            var episode = _episodeRepository.GetEpisodes().FirstOrDefault(m => m.EpisodeId == episodeId);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // GET: Episodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Episodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EpisodeId,EpisodeName")] Episode episode)
        {
            if (ModelState.IsValid)
            {
                _episodeRepository.AddEpisode(episode);
                return RedirectToAction(nameof(Index));
            }
            return View(episode);
        }

        // GET: Episodes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int episodeId = (int)id;
            var episode = _episodeRepository.GetEpisode(episodeId);
            if (episode == null)
            {
                return NotFound();
            }
            return View(episode);
        }

        // POST: Episodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EpisodeId,EpisodeName")] Episode episode)
        {
            if (id != episode.EpisodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _episodeRepository.UpdateEpisode(episode);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodeExists(episode.EpisodeId))
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
            return View(episode);
        }

        // GET: Episodes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodeId = (int)id;
            var episode = _episodeRepository.GetEpisode(episodeId);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // POST: Episodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var episode = _episodeRepository.GetEpisode(id);
            if (episode != null)
            {
                _episodeRepository.DeleteEpisode(episode);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodeExists(int id)
        {
            return _episodeRepository.GetEpisodes().Any(e => e.EpisodeId == id);
        }
    }
}
