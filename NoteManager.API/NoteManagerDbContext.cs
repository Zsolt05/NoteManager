using NoteManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace NoteManager.API
{
    public class NoteManagerDbContext : DbContext
    {
        public NoteManagerDbContext(DbContextOptions<NoteManagerDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
