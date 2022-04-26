using Microsoft.EntityFrameworkCore;
using Santi.Game.App.Models;

namespace Santi.Game.App.Database
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Palavra> Palavras { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
