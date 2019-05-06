using StarWarsPoC.Core.Domain;
using System.Collections.Generic;

namespace StarWarsPoC.Core.Repositories
{
    public interface IEpisodeRepository
    {
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisode(int id);
        void AddEpisode(Episode episode);
        void UpdateEpisode(Episode episode);
        void DeleteEpisode(Episode episode);
    }
}
