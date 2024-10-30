using Notes.Common;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Notes.ViewModels
{
    public class NoteViewModel : ReactiveObject
    {
        public NoteVM NoteVM { get; set; } = new();

        public ReactiveCommand<NoteVM, Unit> UpdateNoteCommand { get; set; }
        public ReactiveCommand<NoteVM, Unit> DeleteNoteCommand { get; set; }

        INotesRepository _notesRepository;
        ObservableCollection<NoteVM> _notes;
        public NoteViewModel(INotesRepository notesRepository, ObservableCollection<NoteVM> notes, NoteVM noteVM)
        {
            _notesRepository = notesRepository;
            _notes = notes;
            NoteVM = noteVM;

            UpdateNoteCommand = ReactiveCommand.Create<NoteVM>(UpdateNote);
            DeleteNoteCommand = ReactiveCommand.Create<NoteVM>(DeleteNote);
        }

        async void UpdateNote(NoteVM noteVM)
        {
            _notesRepository.UpdateNote(noteVM);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        async void DeleteNote(NoteVM noteVM)
        {
            await _notesRepository.DeleteNote(noteVM.Id);
            _notes.Remove(noteVM);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}