using Microsoft.AspNetCore.Mvc.Rendering;
using StarWarsPoC.Core.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWarsPoC.Core.ViewModels
{
    public class AddFriendToCharacterViewModel
    {
        [Required]
        [Display(Name = "Character")]
        public int FriendId { get; set; }

        [Required]
        public int CharacterId { get; set; }

        public Character Character { get; set; }
        public List<SelectListItem> Friends { get; set; }

        public AddFriendToCharacterViewModel() { }

        public AddFriendToCharacterViewModel(Character character, IEnumerable<Character> friends)
        {
            Friends = new List<SelectListItem>();
            Character = character;

            foreach (var friend in friends)
            {
                Friends.Add(new SelectListItem
                {
                    Value = friend.Id.ToString(),
                    Text = friend.Name
                });
            }
        }
    }
}
