using System.ComponentModel.DataAnnotations;

namespace StarWarsPoC.Core.ViewModels
{
    public class AddCharacterViewModel
    {
        [Required]
        [Display(Name = "Character Name")]
        public string Name { get; set; }
    }
}
