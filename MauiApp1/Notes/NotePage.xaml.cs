namespace MauiApp1.Notes;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
	public string ItemId
	{
		set { LoadNote(value); }
	}

	public NotePage()
	{
		InitializeComponent();

		string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

		LoadNote(Path.Combine(appDataPath, randomFileName));
    }

	private void LoadNote(string filename)
	{
		Note notemodel = new Note();
		notemodel.Filename = filename;

		if (File.Exists(filename))
		{
			notemodel.Date = File.GetCreationTime(filename);
			notemodel.Text = File.ReadAllText(filename);
		}
		BindingContext = notemodel;
	}

	private async void SaveButton_Clicked(object sender, EventArgs e)
	{
		// save file
		if (BindingContext is Note note)
            File.WriteAllText(note.Filename, TextEditor.Text);

		await Shell.Current.GoToAsync("..");
    }

	private async void DeleteButton_Clicked(object sender, EventArgs e)
	{
		// delete file
		if (BindingContext is Note note)
		{
			if (File.Exists(note.Filename))
				File.Delete(note.Filename);
		}

        await Shell.Current.GoToAsync("..");
    }
}