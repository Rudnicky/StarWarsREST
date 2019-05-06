using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using StarWarsPoC.Persistence.EntityConfigurations;
using System.Collections;
using System.Linq;

namespace StarWarsPoC.Persistence.Repositories
{
    public class CharacterFriendRepository : ICharacterFriendRepository
    {
        private readonly StarWarsDbContext _context;

        public CharacterFriendRepository(StarWarsDbContext context)
        {
            _context = context;
        }

        public IEnumerable GetCharacterFriends()
        {
            return _context.CharacterFriends;
        }

        public IQueryable<CharacterFriend> GetQueryableCharacterFriends()
        {
            return _context.CharacterFriends;
        }

        public void AddCharacterFriend(CharacterFriend characterFriend)
        {
            _context.CharacterFriends.Add(characterFriend);
            _context.SaveChanges();
        }
    }
}
