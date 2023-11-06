using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Todo.DataAccess.Entities;

public class User : IdentityUser<long>
{
    private ICollection<PersonalTask> _tasks;
    
    public ILazyLoader LazyLoader { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public GenderEnum Gender { get; set; }

    public ICollection<PersonalTask> Tasks
    {
        get => LazyLoader.Load(this, ref _tasks);
        set => _tasks = value;
    }

    public User() { }
    public User(ILazyLoader lazyLoader)
    {
        LazyLoader = lazyLoader;
    }
}

public enum GenderEnum // todo: replace it
{
    Male,
    Female,
    Other,
}