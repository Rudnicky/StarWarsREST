using System.Collections.Generic;

namespace StarWarsPoC.Core.Domain
{
    public class Episode
    {
        public int EpisodeId { get; set; }
        public string EpisodeName { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisode { get; set; } = new List<CharacterEpisode>();
    }
}
