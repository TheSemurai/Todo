namespace TodoAPI.Models;

public class TaskItem
{
    public long Id { get; init; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsComplete { get; set; }
}