using Notes.Common;
using Notes.Views;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Notes.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        NoteVM _selectedNote;
        DataContext dbContext = new DataContext();
        INotesRepository notesRepository = new NotesRepository();

        public ObservableCollection<NoteVM> Notes { get; set; } = new();
        public NoteVM SelectedNote
        {
            get => _selectedNote;
            set
            {
                if (_selectedNote != value)
                {
                    this.RaiseAndSetIfChanged(ref _selectedNote, value);
                    OpenNote(value);
                }
            }
        }

        public ReactiveCommand<Unit,Unit> OpenPageAddNoteCommand { get; set; }

        public MainViewModel()
        {
            dbContext.Database.EnsureCreated();

            var notes = notesRepository.GetAllNotes();

            foreach(var note in notes)
            {
                var noteVM = new NoteVM
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content
                };
                Notes.Add(noteVM);
            }

            OpenPageAddNoteCommand = ReactiveCommand.Create(OpenPageAddNote);
        }

        async void OpenPageAddNote()
        {
            var vm = new AddNoteViewModel(notesRepository, Notes);
            var page = new AddNotePage(vm);
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        void OpenNote(NoteVM noteVM)
        {
            var vm = new NoteViewModel(notesRepository, Notes, noteVM);
            var page = new NotePage(vm);
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
    }
}