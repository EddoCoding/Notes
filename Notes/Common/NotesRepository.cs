using Microsoft.EntityFrameworkCore;
using Notes.Models;
using Notes.ViewModels;

namespace Notes.Common
{
    public class NotesRepository : INotesRepository
    {
        public IEnumerable<Note> GetAllNotes()
        {
            using(var dbContext = new DataContext())
            {
                return dbContext.Notes.ToArray();
            }
        }

        public async Task CreateNote(Note note)
        {
            using (var dbContext = new DataContext())
            {
                await dbContext.Notes.AddAsync(note);
                dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateNote(NoteVM noteVM)
        {
            using (var dbContext = new DataContext())
            {
                var note = await dbContext.Notes.FirstOrDefaultAsync(x => x.Id == noteVM.Id);
                if(note != null)
                {
                    note.Title = noteVM.Title;
                    note.Content = noteVM.Content;
                }
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteNote(Guid id)
        {
            using (var dbContext = new DataContext())
            {
                await dbContext.Notes
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();
            }
        }
    }
}