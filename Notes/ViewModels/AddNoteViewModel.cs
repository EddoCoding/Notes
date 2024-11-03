using Notes.Common;
using Notes.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Notes.ViewModels
{
    public class AddNoteViewModel : ReactiveObject
    {
        public NoteVM NoteVM { get; set; } = new();

        public ReactiveCommand<NoteVM, Unit> AddNoteCommand { get; set; }

        INotesRepository _notesRepository;
        ObservableCollection<NoteVM> notes;
        List<NoteVM> _notes;
        public AddNoteViewModel(INotesRepository notesRepository, ObservableCollection<NoteVM> Notes, List<NoteVM> notes)
        {
            _notesRepository = notesRepository;
            this.notes = Notes;
            this._notes = notes;

            AddNoteCommand = ReactiveCommand.Create<NoteVM>(AddNote);
        }

        async void AddNote(NoteVM noteVM)
        {
            if (!noteVM.Validation()) return;

            var note = new Note
            {
                Id = noteVM.Id,
                Title = noteVM.Title,
                Content = noteVM.Content
            };
            await _notesRepository.CreateNote(note);
            notes.Add(noteVM);
            _notes.Add(noteVM);

            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}