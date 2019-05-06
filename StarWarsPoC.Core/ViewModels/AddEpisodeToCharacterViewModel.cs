using Microsoft.AspNetCore.Mvc.Rendering;
using StarWarsPoC.Core.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWarsPoC.Core.ViewModels
{
    public class AddEpisodeToCharacterViewModel
    {
        [Required]
        [Display(Name = "Character")]
        public int EpisodeId { get; set; }

        [Required]
        public int CharacterId { get; set; }

        public Character Character { get; set; }
        public List<SelectListItem> Episodes { get; set; }

        public AddEpisodeToCharacterViewModel() { }

        public AddEpisodeToCharacterViewModel(Character character, IEnumerable<Episode> episodes)
        {
            Episodes = new List<SelectListItem>();
            Character = character;

            foreach (var episode in episodes)
            {
                Episodes.Add(new SelectListItem
                {
                    Value = episode.EpisodeId.ToString(),
                    Text = episode.EpisodeName
                });
            }
        }
    }
}
