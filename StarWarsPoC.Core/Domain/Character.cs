using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsPoC.Core.Domain
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisode { get; set; } = new List<CharacterEpisode>();
        public ICollection<CharacterFriend> CharacterFriend { get; set; } = new List<CharacterFriend>();
    }
}
