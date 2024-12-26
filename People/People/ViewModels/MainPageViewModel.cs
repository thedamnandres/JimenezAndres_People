using People.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace People.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private PersonRepository _andresjimenezRepo;
        private string _newPersonName;
        private string _statusMessage;
        private ObservableCollection<Person> _people;

        public string NewPersonName
        {
            get => _newPersonName;
            set
            {
                _newPersonName = value;
                OnPropertyChanged(nameof(NewPersonName));
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
                
                if (!string.IsNullOrEmpty(value))
                {
                    ClearStatusMessageAfterDelay();
                }
                
            }
        }

        public ObservableCollection<Person> People
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged(nameof(People));
            }
        }

        public ICommand AddNewPersonCommand { get; }
        public ICommand GetAllPeopleCommand { get; }
        public ICommand DeletePersonCommand { get; }

        public MainPageViewModel()
        {
            _andresjimenezRepo = new PersonRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "andresjimenez_people.db"));
            People = new ObservableCollection<Person>();

            AddNewPersonCommand = new Command(AddNewPerson);
            GetAllPeopleCommand = new Command(GetAllPeople);
            DeletePersonCommand = new Command<Person>(DeletePerson);

            GetAllPeople();
        }

        private void AddNewPerson()
        {
            _andresjimenezRepo.AddNewPerson(NewPersonName);
            StatusMessage = _andresjimenezRepo.StatusMessage;
            GetAllPeople();
        }

        private void GetAllPeople()
        {
            People.Clear();
            var people = _andresjimenezRepo.GetAllPeople();
            foreach (var person in people)
            {
                People.Add(person);
            }
        }

        private void DeletePerson(Person person)
        {
            _andresjimenezRepo.DeletePerson(person.Id);
            StatusMessage = _andresjimenezRepo.StatusMessage;
            GetAllPeople();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // Mensaje de Estado desaparece en 4 segundos
        private async void ClearStatusMessageAfterDelay()
        {
            await Task.Delay(4000);
            StatusMessage = string.Empty;
        }
    }
}