using Microsoft.EntityFrameworkCore;
using Notes.Models;

namespace Notes.Common
{
    public class DataContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={Path.Combine(FileSystem.AppDataDirectory, "myNotes.db")}");
    }
}