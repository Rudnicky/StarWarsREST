using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using StarWarsPoC.Persistence.EntityConfigurations;
using System.Linq;

namespace StarWarsPoC.Persistence.Repositories
{
    public class CharacterEpisodesRepository : ICharacterEpisodeRepository
    {
        private readonly StarWarsDbContext _context;

        public CharacterEpisodesRepository(StarWarsDbContext context)
        {
            _context = context;
        }

        public IQueryable<CharacterEpisode> GetQueryableCharacterEpisodes()
        {
            return _context.CharacterEpisodes;
        }

        public void AddCharacterEpisode(CharacterEpisode characterEpisode)
        {
            _context.CharacterEpisodes.Add(characterEpisode);
            _context.SaveChanges();
        }
    }
}
