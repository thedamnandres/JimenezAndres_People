using People.Models;
using System.Collections.Generic;

namespace People;

public partial class MainPage : ContentPage
{

    PersonRepository _andresjimenezRepo;
	public MainPage()
	{
		InitializeComponent();
        _andresjimenezRepo = new PersonRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "andresjimenez_people.db"));
        LoadPeople();
        
	}

    public void LoadPeople()
    {
        AJpeopleList.ItemsSource =  _andresjimenezRepo.GetAllPeople();
    }
    public void OnNewButtonClicked(object sender, EventArgs args)
    {
        AJstatusMessage.Text = "";

        App.PersonRepo.AddNewPerson(AJnewPerson.Text);
        AJstatusMessage.Text = App.PersonRepo.StatusMessage;
    }

    public void OnGetButtonClicked(object sender, EventArgs args)
    {
        AJstatusMessage.Text = "";

        List<Person> people = App.PersonRepo.GetAllPeople();
        AJpeopleList.ItemsSource = people;
    }

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item != null && e.Item is Person person)
        {
            bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {person.Name}?", "Yes", "No");
            if (confirm)
            {
                App.PersonRepo.DeletePerson(person.Id);
                await DisplayAlert("Deleted", $"Andres Jimenez acaba de eliminar un registro.", "OK");
                LoadPeople();
            }
        }
    }
}

