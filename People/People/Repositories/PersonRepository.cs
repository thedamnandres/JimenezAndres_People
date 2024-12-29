using People.Models;
using SQLite;

namespace People;

public class PersonRepository
{
    private string _andresjimenezdbPath;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection
    private SQLiteConnection conn;

    private void Init()
    {
        if (conn != null) 
            return;
         
        conn = new SQLiteConnection(_andresjimenezdbPath);
        conn.CreateTable<Person>();
    }

    public PersonRepository(string dbPath)
    {
        _andresjimenezdbPath = dbPath;
    }
    
    public void AddNewPerson(string name)
    {            
        int result = 0;
        try
        {
            // TODO: Call Init()
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Nombre valido requerido");

            // TODO: Insert the new person into the database
            result = conn.Insert(new Person { Name = name });

            StatusMessage = string.Format("{0} persona agregada (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Erro al agregar {0}. Error: {1}", name, ex.Message);
        }

    }

    public List<Person> GetAllPeople()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            Init();
            return conn.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Error al obtener los datos. {0}", ex.Message);
        }

        return new List<Person>();
    }
    
    public void DeletePerson(int id)
    {
        try
        {
            Init();
            var person = conn.Table<Person>().FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                conn.Delete(person);
                StatusMessage = $"Persona {id} eliminada.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error al eliminar a {id}. Error: {ex.Message}";
        }
    }
}
