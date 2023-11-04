using System.ComponentModel.DataAnnotations;

namespace Todo.DataAccess.Entities;

public class User
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public GenderEnum Gender { get; set; }
    public ICollection<PersonalTask> Tasks { get; set; }
}

public enum GenderEnum
{
    Male,
    Female,
    Other,
}