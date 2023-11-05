namespace Todo.BusinessLogic.Entities;

public class TaskItemToUpdate
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool? IsComplete { get; set; }
}