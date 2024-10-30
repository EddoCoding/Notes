using Notes.Models;
using Notes.ViewModels;

namespace Notes.Common
{
    public interface INotesRepository
    {
        IEnumerable<Note> GetAllNotes();
        Task CreateNote(Note note);
        Task UpdateNote(NoteVM noteVM);
        Task DeleteNote(Guid id);
    }
}