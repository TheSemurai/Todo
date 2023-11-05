using System.ComponentModel.DataAnnotations;

namespace Todo.BusinessLogic.Entities;

public class TaskItem
{
    [Key]
    public long? Id { get; init; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool? IsComplete { get; set; } 
}

