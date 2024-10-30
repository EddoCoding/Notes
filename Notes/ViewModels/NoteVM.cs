using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Notes.ViewModels
{
    public class NoteVM : ReactiveObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Reactive] public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public bool Validation()
        {
            if (String.IsNullOrWhiteSpace(Title))
            {
                Application.Current.MainPage.DisplayAlert("Валидация", "Заголовок не указан!", "Ок");
                return false;
            }

            return true;
        }
    }
}