using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using StarWarsPoC.Persistence.EntityConfigurations;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsPoC.Persistence.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly StarWarsDbContext _context;

        public EpisodeRepository(StarWarsDbContext context)
        {
            _context = context;
        }

        public void AddEpisode(Episode episode)
        {
            _context.Episodes.Add(episode);
            _context.SaveChanges();
        }

        public void DeleteEpisode(Episode episode)
        {
            _context.Episodes.Remove(episode);
            _context.SaveChanges();
        }

        public Episode GetEpisode(int id)
        {
            return _context.Episodes.Single(c => c.EpisodeId == id);
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return _context.Episodes;
        }

        public void UpdateEpisode(Episode episode)
        {
            _context.Episodes.Update(episode);
            _context.SaveChanges();
        }
    }
}
