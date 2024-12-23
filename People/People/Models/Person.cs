using SQLite;

namespace People.Models; 

[Table("people")]
public class Person
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [MaxLength(50), Unique]
    public string Name { get; set; }
}