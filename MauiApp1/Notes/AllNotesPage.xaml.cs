namespace MauiApp1.Notes;

public partial class AllNotesPage : ContentPage
{
	public AllNotesPage()
	{
		InitializeComponent();

		BindingContext = new AllNotes();
	}

	protected override void OnAppearing()
	{
		((AllNotes)BindingContext).LoadNotes();
	}

	private async void noteCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count != 0)
		{
			// get the note model
			var note = (Note)e.CurrentSelection[0];

            // navigate to the note selected "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.Filename}");
		}
	}

	private async void Add_Clicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync(nameof(NotePage));
    }
}