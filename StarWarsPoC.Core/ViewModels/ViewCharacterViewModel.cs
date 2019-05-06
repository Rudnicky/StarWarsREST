using StarWarsPoC.Core.Domain;
using System.Collections.Generic;

namespace StarWarsPoC.Core.ViewModels
{
    public class ViewCharacterViewModel
    {
        public Character Character { get; set; }
        public IList<CharacterEpisode> Items { get; set; }
        public IList<Character> Friends { get; set; }
        public int NumberOfAvailableFriends { get; set; }
    }
}
