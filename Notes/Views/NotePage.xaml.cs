using Notes.ViewModels;

namespace Notes.Views;

public partial class NotePage : ContentPage
{
	public NotePage(NoteViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}