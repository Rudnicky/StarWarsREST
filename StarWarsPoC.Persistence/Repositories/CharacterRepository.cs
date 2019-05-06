using StarWarsPoC.Core.Domain;
using StarWarsPoC.Core.Repositories;
using StarWarsPoC.Persistence.EntityConfigurations;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsPoC.Persistence.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly StarWarsDbContext _context;

        public CharacterRepository(StarWarsDbContext context)
        {
            _context = context;
        }

        public void AddCharacter(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();
        }

        public void DeleteCharacter(Character character)
        {
            _context.Characters.Remove(character);
            _context.SaveChanges();
        }

        public Character GetCharacter(int id)
        {
            return _context.Characters.Single(c => c.Id == id);
        }

        public IEnumerable<Character> GetCharacters()
        {
            return _context.Characters;
        }

        public IQueryable<Character> GetQueryableCharacters()
        {
            return _context.Characters;
        }

        public void UpdateCharacter(Character character)
        {
            _context.Update(character);
            _context.SaveChanges();
        }
    }
}
