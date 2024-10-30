using Notes.ViewModels;

namespace Notes.Views;

public partial class AddNotePage : ContentPage
{
	public AddNotePage(AddNoteViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}