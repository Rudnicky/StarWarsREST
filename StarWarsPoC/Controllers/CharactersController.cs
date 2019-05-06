using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using StarWarsPoC.Core.ViewModels;
using StarWarsPoC.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsPoC.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly ICharacterEpisodeRepository _characterEpisodesRepository;
        private readonly ICharacterFriendRepository _characterFriendsRepository;

        public CharactersController(
            ICharacterRepository characterRepository, 
            IEpisodeRepository episodeRepository,
            ICharacterEpisodeRepository characterEpisodesRepository,
            ICharacterFriendRepository characterFriendsRepository)
        {
            _characterRepository = characterRepository;
            _episodeRepository = episodeRepository;
            _characterEpisodesRepository = characterEpisodesRepository;
            _characterFriendsRepository = characterFriendsRepository;
        }

        // GET: Characters or Characters?pageNumber=1&pageSize=5
        [HttpGet("/Characters"), ActionName("Index")]
        public async Task<IActionResult> GetCharacters(int? pageNumber, int? pageSize)
        {
            int currentPage = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 10;

            var paginatedCharacters = await PaginatedList<Character>.CreateAsync(_characterRepository.GetQueryableCharacters(), currentPage, currentPageSize); 
            return View(paginatedCharacters);
        }

        [HttpGet("/Characters/Details/"), ActionName("Details")]
        public IActionResult GetDetailedCharacter(int id)
        {
            var character = _characterRepository.GetCharacter(id);
            var items = _characterEpisodesRepository.GetQueryableCharacterEpisodes()
                .Include(item => item.Episode)
                .Where(cm => cm.CharacterId == id)
                .ToList();

            var friends = new List<Character>();
            var characters = _characterRepository.GetCharacters().ToList();
            var characterFriends = _characterFriendsRepository.GetQueryableCharacterFriends()
                .Where(f => f.CharacterId == id)
                .ToList();

            if (characters != null && characterFriends != null && characterFriends.Count > 0)
            {
                foreach (var friend in characterFriends)
                {
                    var characterFriend = characters[friend.FriendId];
                    if (characterFriend != null)
                    {
                        friends.Add(characterFriend);
                    }
                }
            }

            var viewCharacterViewModel = new ViewCharacterViewModel
            {
                Character = character,
                Items = items,
                Friends = friends,
                NumberOfAvailableFriends = characters.Count
            };

            return View(viewCharacterViewModel);
        }

        [HttpGet("/Characters/Create/"), ActionName("Create")]
        public IActionResult CreateCharacter()
        {
            var addCharacterViewModel = new AddCharacterViewModel();
            return View(addCharacterViewModel);
        }

        [HttpPost("/Characters/Create/"), ActionName("Create")]
        public IActionResult CreateCharacter(AddCharacterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var character = new Character()
                {
                    Name = viewModel.Name
                };
                _characterRepository.AddCharacter(character);

                var addedCharacter = _characterRepository.GetCharacters().FirstOrDefault(x => x.Name == viewModel.Name);
                return Redirect("/Characters/Details?id=" + character.Id);
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult AddEpisode(int id)
        {
            var character = _characterRepository.GetCharacter(id);
            var episodes = _episodeRepository.GetEpisodes().ToList();
            var addEpisodeToCharacterViewModel = new AddEpisodeToCharacterViewModel(character, episodes);
            return View(addEpisodeToCharacterViewModel);
        }

        [HttpPost]
        public IActionResult AddEpisode(AddEpisodeToCharacterViewModel addEpisodeToCharacterViewModel)
        {
            if (ModelState.IsValid)
            {
                var episodeId = addEpisodeToCharacterViewModel.EpisodeId;
                var characterId = addEpisodeToCharacterViewModel.CharacterId;

                var existingItems = _characterEpisodesRepository.GetQueryableCharacterEpisodes()
                    .Where(cm => cm.EpisodeId == episodeId)
                    .Where(cm => cm.CharacterId == characterId)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    var newCharacterEpisode = new CharacterEpisode
                    {
                        EpisodeId = episodeId,
                        CharacterId = characterId
                    };
                    _characterEpisodesRepository.AddCharacterEpisode(newCharacterEpisode);
                    return Redirect("/Characters/Details?id=" + characterId);
                }
                else
                {
                    return Redirect("/Characters/Details?id=" + characterId);
                }
            }
            else
            {
                return View(addEpisodeToCharacterViewModel);
            }
        }

        [HttpGet]
        public IActionResult AddFriend(int id)
        {
            var character = _characterRepository.GetCharacter(id);
            var characters = _characterRepository.GetCharacters().ToList();

            // remove current character from the list
            // cuz it simply can't reffer to itself!
            characters.Remove(character);

            var addEpisodeToCharacterViewModel = new AddFriendToCharacterViewModel(character, characters);
            return View(addEpisodeToCharacterViewModel);
        }

        [HttpPost]
        public IActionResult AddFriend(AddFriendToCharacterViewModel addFriendToCharacterViewModel)
        {
            if (ModelState.IsValid)
            {
                var friendId = addFriendToCharacterViewModel.FriendId - 1;
                var characterId = addFriendToCharacterViewModel.CharacterId;
                var existingItems = _characterFriendsRepository.GetQueryableCharacterFriends()
                    .Where(cm => cm.FriendId == friendId)
                    .Where(cm => cm.CharacterId == characterId)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    var newCharacterFriend = new CharacterFriend
                    {
                        FriendId = friendId,
                        CharacterId = characterId
                    };
                    _characterFriendsRepository.AddCharacterFriend(newCharacterFriend);
                    return Redirect("/Characters/Details?id=" + characterId);
                }
                else
                {
                    return Redirect("/Characters/Details?id=" + characterId);
                }
            }
            else
            {
                return View(addFriendToCharacterViewModel);
            }
        }

        [HttpGet("/Characters/Edit/"), ActionName("Edit")]
        public IActionResult EditCharacter(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int characterId = (int)id;
            var character = _characterRepository.GetCharacter(characterId);
            if (character == null)
            {
                return NotFound();
            }
            return View(character);
        }

        [HttpPost("/Characters/Edit/"), ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditCharacter(int id, [Bind("Id,Name")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _characterRepository.UpdateCharacter(character);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(character);
        }

        [HttpGet("/Characters/Delete/"), ActionName("Delete")]
        public IActionResult DeleteCharacter(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterId = (int)id;
            var character = _characterRepository.GetCharacter(characterId);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        [HttpPost("/Characters/Delete/"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var character = _characterRepository.GetCharacter(id);
            if (character != null)
            {
                _characterRepository.DeleteCharacter(character);
            }
            return RedirectToAction("Index");
        }

        private bool CharacterExists(int id)
        {
            return _characterRepository.GetCharacters().Any(e => e.Id == id);
        }
    }
}
