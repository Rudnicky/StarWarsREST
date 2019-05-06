using StarWarsPoC.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsPoC.Core.Repositories
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> GetCharacters();
        IQueryable<Character> GetQueryableCharacters();
        Character GetCharacter(int id);
        void AddCharacter(Character character);
        void UpdateCharacter(Character character);
        void DeleteCharacter(Character character);
    }
}
