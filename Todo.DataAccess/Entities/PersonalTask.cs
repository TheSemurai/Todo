using System.ComponentModel.DataAnnotations;

namespace Todo.DataAccess.Entities;

public class PersonalTask
{
    [Key]
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsComplete { get; set; }
    public User Author { get; set; }
}