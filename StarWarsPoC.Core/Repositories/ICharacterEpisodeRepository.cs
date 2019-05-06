using StarWarsPoC.Core.Domain;
using System.Linq;

namespace StarWarsPoC.Core.Repositories
{
    public interface ICharacterEpisodeRepository
    {
        IQueryable<CharacterEpisode> GetQueryableCharacterEpisodes();
        void AddCharacterEpisode(CharacterEpisode characterEpisode);
    }
}
