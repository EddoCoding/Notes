using Notes.Common;
using Notes.Views;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Notes.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        DataContext dbContext = new DataContext();
        INotesRepository notesRepository = new NotesRepository();
        string textSearch = string.Empty;
        NoteVM _selectedNote;
        List<NoteVM> notes = new();

        public string TextSearch
        {
            get => textSearch;
            set
            {
                this.RaiseAndSetIfChanged(ref textSearch, value);
                if (notes != null && !String.IsNullOrWhiteSpace(textSearch)) Search();
                if (String.IsNullOrWhiteSpace(textSearch)) Search();
            }
        }
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

            GetNotes();

            OpenPageAddNoteCommand = ReactiveCommand.Create(OpenPageAddNote);
        }

        void GetNotes()
        {
            foreach (var note in notesRepository.GetAllNotes())
            {
                var noteVM = new NoteVM
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content
                };
                Notes.Add(noteVM);
            }
            notes.AddRange(Notes);
        }
        async void OpenPageAddNote()
        {
            var vm = new AddNoteViewModel(notesRepository, Notes, notes);
            var page = new AddNotePage(vm);
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
        async void OpenNote(NoteVM noteVM)
        {
            var vm = new NoteViewModel(notesRepository, Notes, notes, noteVM);
            var page = new NotePage(vm);
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
        void Search()
        {
            var items = notes.FindAll(x => x.Title.IndexOf(textSearch) >= 0);
            Notes.Clear();
            foreach (var item in items) Notes.Add(item);
        }
    }
}