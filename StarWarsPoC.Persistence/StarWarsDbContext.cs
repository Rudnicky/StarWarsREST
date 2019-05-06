using Microsoft.EntityFrameworkCore;
using StarWarsPoC.Core.Domain;

namespace StarWarsPoC.Persistence.EntityConfigurations
{
    public class StarWarsDbContext : DbContext
    {
        public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options)
            : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<CharacterEpisode> CharacterEpisodes { get; set; }
        public DbSet<CharacterFriend> CharacterFriends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterEpisode>()
                .HasKey(k => new { k.CharacterId, k.EpisodeId });

            modelBuilder.Entity<CharacterFriend>()
                .HasKey(k => new { k.CharacterFriendId, k.CharacterId, k.FriendId });
        }
    }
}
