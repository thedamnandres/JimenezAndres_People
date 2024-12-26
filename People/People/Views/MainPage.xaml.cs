using People.Models;
using People.ViewModels;

namespace People.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is Person person)
            {
                bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {person.Name}?", "Yes", "No");
                if (confirm)
                {
                    ((MainPageViewModel)BindingContext).DeletePersonCommand.Execute(person);
                    await DisplayAlert("Deleted", $"Andres Jimenez acaba de eliminar un registro.", "OK");
                }
            }
        }
    }
}