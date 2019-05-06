using StarWarsPoC.Core.Domain;
using System.Collections;
using System.Linq;

namespace StarWarsPoC.Core.Repositories
{
    public interface ICharacterFriendRepository
    {
        IEnumerable GetCharacterFriends();
        IQueryable<CharacterFriend> GetQueryableCharacterFriends();
        void AddCharacterFriend(CharacterFriend characterFriend);
    }
}
