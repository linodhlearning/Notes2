
using Microsoft.EntityFrameworkCore;
namespace Notes.Api.Repository.Entities
{
    public class NotesDBContext : DbContext
    {
        public NotesDBContext(DbContextOptions<NotesDBContext> options) : base(options)
        { 

        }
        public DbSet<Note> Notes { get; set; } 
    }
}
